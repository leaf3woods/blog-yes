using BlogYes.Domain.Entities.Base;

namespace BlogYes.Domain.Entities.Base
{
    public class IncrementEntity : IAggregateRoot
    {
        public long? Id { get; set; } = null!;
    }
}