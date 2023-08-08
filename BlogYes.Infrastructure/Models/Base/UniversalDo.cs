using System.ComponentModel.DataAnnotations;

namespace BlogYes.Infrastructure.Models.Base
{
    public class UniversalDo
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}