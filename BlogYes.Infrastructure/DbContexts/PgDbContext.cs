using BlogYes.Infrastructure.Models;
using BlogYes.Infrastructure.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace BlogYes.Infrastructure.DbContexts
{
    public class PgDbContext : DbContext
    {
        public PgDbContext()
        {

        }

        #region dbsets

        public DbSet<UserDo> Users { get; set; }
        public DbSet<BlogDo> Blogs { get; set; }
        public  DbSet<CommentDo> Comments { get; set; }
        public  DbSet<CategoryDo> Categories { get; set; }

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

            modelBuilder.Entity<BlogDo>()
                .HasMany(b => b.Comments)
                .WithOne(c => c.Blog)
                .HasForeignKey(c => c.BlogId);

            modelBuilder.Entity<BlogDo>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Blogs)
                .HasForeignKey(b => b.CategoryName);

            modelBuilder.Entity<UserDo>()
                .HasMany(u => u.Blogs)
                .WithOne(b => b.UserDo)
                .HasForeignKey(b => b.UserId);

            #endregion

        }
    }
}