using System.ComponentModel.DataAnnotations;

namespace BlogYes.Infrastructure.Models
{
    public class CategoryDo
    {
        [Key]
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
    }
}