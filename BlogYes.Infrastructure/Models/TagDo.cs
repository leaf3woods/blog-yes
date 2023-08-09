using Microsoft.EntityFrameworkCore;

namespace BlogYes.Infrastructure.Models
{
    [Owned]
    public class TagDo
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}