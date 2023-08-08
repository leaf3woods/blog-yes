using BlogYes.Infrastructure.Models.Base;

namespace BlogYes.Infrastructure.Models
{
    public class CommentDo : UniversalDo
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