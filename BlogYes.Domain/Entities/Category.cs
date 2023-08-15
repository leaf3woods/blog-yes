using BlogYes.Domain.Entities.Base;

namespace BlogYes.Domain.Entities
{
    public class Category : IncrementEntity, ISoftDelete
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreateTime { get; set; }

        #region navigation
        public virtual ICollection<Blog> Blogs { get; set; } = null!;
        #endregion

        #region filter
        public bool SoftDeleted { get; set; } = false;
        #endregion
    }
}