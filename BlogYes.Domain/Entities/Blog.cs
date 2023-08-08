using BlogYes.Domain.Entities.Base;
using BlogYes.Domain.ValueObjects.Blog;

namespace BlogYes.Domain.Entities
{
    public class Blog : IncrementEntity
    {
        public Guid UserId { get; set; }
        public Category? Category { get; set; } = null!;
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime ModifyTime { get; set; }
        public BlogState State { get; set; }
    }

    public enum BlogState
    {
        Draft,
        Publish
    }
}