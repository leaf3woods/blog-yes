using BlogYes.Domain.Entities.Base;
using BlogYes.Domain.ValueObjects.BlogValue;

namespace BlogYes.Domain.Entities
{
    public class Blog : IncrementEntity, ISoftDelete
    {
        public ICollection<Tag>? Tags { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime ModifyTime { get; set; }
        public DateTime CreateTime { get; set; }
        public BlogState State { get; set; }

        #region navigation
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public long CategoryId { get; set; }
        public virtual Category? Category { get; set; } = null!;
        public ICollection<Comment>? Comments { get; set; }
        #endregion

        #region filter
        public bool SoftDeleted { get; set; } = false;
        #endregion
    }

    public enum BlogState
    {
        Draft,
        Publish
    }
}