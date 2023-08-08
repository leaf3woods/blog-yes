using BlogYes.Domain.Entities.Base;

namespace BlogYes.Domain.Entities
{
    public class Comment : IncrementEntity
    {
        public long BlogId { get; set; }
        public long? ParentId { get; set; }
        public DateTime Time { get; set; }
        public long Like { get; set; }
        public long Star { get; set; }
        public string UserName { get; set; } = null!;
        public Guid UserId { get; set; }
    }
}