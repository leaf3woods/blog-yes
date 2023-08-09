using BlogYes.Infrastructure.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BlogYes.Infrastructure.Models
{
    [Index(nameof(SoftDeleted))]
    public class CategoryDo : ISoftDelete
    {
        [Key]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
        public ICollection<BlogDo> Blogs { get; set; } = new List<BlogDo>();

        #region filter
        public bool SoftDeleted { get; set; } = false;
        #endregion
    }
}