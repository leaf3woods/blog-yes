
namespace BlogYes.Infrastructure.Models.Base
{
    public interface ISoftDelete
    {
        public bool SoftDeleted { get; set; }
    }
}
