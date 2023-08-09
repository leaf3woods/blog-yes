using BlogYes.Infrastructure.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace BlogYes.Infrastructure.Models
{
    [Index(nameof(UserId))]
    [Index(nameof(Title))]
    [Index(nameof(ModifyTime))]
    [Index(nameof(SoftDeleted))]
    public class BlogDo : IncrementDo, ISoftDelete
    {
        public Guid UserId { get; set; }
        public UserDo UserDo { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public CategoryDo? Category { get; set; } = null!;
        public ICollection<TagDo> Tags { get; set; } = new List<TagDo>();
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime ModifyTime { get; set; }
        public ICollection<CommentDo> Comments { get; set; } = new List<CommentDo>();
        public int State { get; set; }

        #region filter
        public bool SoftDeleted { get; set; } = false;
        #endregion
    }
}