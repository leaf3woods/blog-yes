using BlogYes.Domain.Entities;
using BlogYes.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace BlogYes.Infrastructure.DbContexts
{
    public class PgDbContext : DbContext
    {
        public PgDbContext(DbContextOptions<PgDbContext> options) : base(options) { }

        #region dbsets

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public  DbSet<Comment> Comment { get; set; }
        public  DbSet<Category> Category { get; set; }

        #endregion

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entityEntry in ChangeTracker.Entries())
            {

                if (entityEntry.State == EntityState.Deleted && entityEntry.Entity is ISoftDelete delete)
                {
                    entityEntry.State = EntityState.Unchanged;
                    delete.SoftDeleted = true;
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

            #region user
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Password);

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
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .OwnsMany(r => r.Scopes)
                .WithOwner(sc => sc.Role);

            modelBuilder.Entity<Role>()
                .HasMany(u => u.Users)
                .WithOne(b => b.Role)
                .HasForeignKey(b => b.RoleId);
            #endregion

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

            #endregion
        }
    }
}