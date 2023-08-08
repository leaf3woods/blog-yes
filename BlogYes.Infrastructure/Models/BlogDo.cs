using BlogYes.Infrastructure.Models.Base;

namespace BlogYes.Infrastructure.Models
{
    public class BlogDo : IncrementDo
    {
        public Guid UserId { get; set; }
        public UserDo UserDo { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public CategoryDo? Category { get; set; } = null!;
        public List<TagDo> Tags { get; set; } = new List<TagDo>();
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime ModifyTime { get; set; }
        public int State { get; set; }
    }
}