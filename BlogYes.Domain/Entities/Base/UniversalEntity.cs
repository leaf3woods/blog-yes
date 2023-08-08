using BlogYes.Domain.Entities.Base;

namespace BlogYes.Domain.Entities.Base
{
    public class UniversalEntity : IAggregateRoot
    {
        public Guid? Id { get; set; }
    }
}