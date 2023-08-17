using BlogYes.Domain.Entities.Base;
using BlogYes.Domain.ValueObjects.UserValue;

namespace BlogYes.Domain.Entities
{
    public class Role : UniversalEntity, ISoftDelete
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        #region navigation

        public ICollection<Scope> Scopes { get; set; } = null!;
        public virtual ICollection<User>? Users { get; set; }

        #endregion navigation

        #region delete filter

        public bool SoftDeleted { get; set; } = false;
        public DateTime? DeleteTime { get; set; } = null;

        #endregion delete filter
    }
}