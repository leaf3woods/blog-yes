using BlogYes.Domain.Entities.Base;

namespace BlogYes.Domain.Entities
{
    public class Comment : IncrementEntity, ISoftDelete
    {
        public long? ParentId { get; set; }
        public DateTime PostTime { get; set; }
        public long Like { get; set; }
        public long Star { get; set; }

        #region navigation
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public long BlogId { get; set; }
        public virtual Blog Blog { get; set; } = null!;
        #endregion

        #region delete filter
        public bool SoftDeleted { get; set; } = false;
        public DateTime? DeleteTime { get; set; } = null;
        #endregion
    }
}