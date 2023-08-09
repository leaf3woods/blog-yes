using BlogYes.Infrastructure.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace BlogYes.Infrastructure.Models
{
    [Index(nameof(SoftDeleted))]
    public class CommentDo : IncrementDo, ISoftDelete
    {
        public long BlogId { get; set; }
        public BlogDo Blog { get; set; } = null!;
        public long? ParentId { get; set; }
        public DateTime Time { get; set; }
        public long Like { get; set; }
        public long Star { get; set; }
        public string UserName { get; set; } = null!;
        public Guid UserId { get; set; }

        #region filter
        public bool SoftDeleted { get; set; } = false;
        #endregion
    }
}