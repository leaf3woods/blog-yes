
namespace BlogYes.Domain.Entities.Base
{
    public interface ISoftDelete
    {
        public bool SoftDeleted { get; set; }
    }
}
