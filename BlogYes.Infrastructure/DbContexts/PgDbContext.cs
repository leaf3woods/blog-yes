using BlogYes.Domain.Entities;
using BlogYes.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace BlogYes.Infrastructure.DbContexts
{
    public class PgDbContext : DbContext
    {
        public PgDbContext(DbContextOptions<PgDbContext> options) : base(options)
        {

        }

        #region dbsets

        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public  DbSet<Comment> Comments { get; set; }
        public  DbSet<Category> Categories { get; set; }

        #endregion

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entityEntry in ChangeTracker.Entries())
            {
                if (entityEntry.State == EntityState.Deleted && entityEntry.GetType().IsAssignableTo(typeof(ISoftDelete)))
                {
                    entityEntry.State = EntityState.Unchanged;
                    entityEntry.Property("SoftDeleted").CurrentValue = true;
                    entityEntry.Property("SoftDeleted").IsModified = true;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region soft delete filter
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                {
                    entityType.AddSoftDeleteQueryFilter();
                }
            }
            #endregion


            #region entities relationship

            #region blog
            modelBuilder.Entity<Blog>()
                .HasIndex(b => b.ModifyTime)
                .IsDescending();

            modelBuilder.Entity<Blog>()
                .HasIndex(b => b.CreateTime)
                .IsDescending();

            modelBuilder.Entity<Blog>()
                .HasIndex(b => b.SoftDeleted);

            modelBuilder.Entity<Blog>()
                .OwnsMany(b => b.Tags);

            modelBuilder.Entity<Blog>()
                .HasMany(b => b.Comments)
                .WithOne(c => c.Blog)
                .HasForeignKey(c => c.BlogId);

            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Blogs)
                .HasForeignKey(b => b.CategoryId);
            #endregion

            #region comment
            modelBuilder.Entity<Comment>()
                .HasIndex(c => c.SoftDeleted);

            modelBuilder.Entity<Comment>()
                .HasIndex(c => c.PostTime)
                .IsDescending();
            #endregion

            #region category
            modelBuilder.Entity<Category>()
                .HasIndex(b => b.SoftDeleted);

            modelBuilder.Entity<Category>()
                .HasIndex(b => b.CreateTime);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Blogs)
                .WithOne(b => b.Category)
                .HasForeignKey(b => b.CategoryId);
            #endregion

            #region user
            modelBuilder.Entity<User>()
                .HasIndex(u => u.SoftDeleted);

            modelBuilder.Entity<User>()
                .OwnsOne(u => u.Settings);

            modelBuilder.Entity<User>()
                .OwnsOne(u => u.Detail);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Blogs)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);
            #endregion

            #region role
            modelBuilder.Entity<Role>()
                .HasIndex(u => u.SoftDeleted);

            modelBuilder.Entity<Role>()
                .HasMany(u => u.Users)
                .WithOne(b => b.Role)
                .HasForeignKey(b => b.RoleId);
            #endregion

            #endregion

        }
    }
}