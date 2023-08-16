using BlogYes.Domain.Entities.Base;
using BlogYes.Domain.ValueObjects.User;

namespace BlogYes.Domain.Entities
{
    public class Role : UniversalEntity, ISoftDelete
    {
        public string Name { get; set; } = null!;
        public ICollection<Scope> Scopes { get; set; } = null!;
        #region navigation
        public virtual ICollection<User>? Users { get; set; }
        #endregion

        #region filter
        public bool SoftDeleted { get; set; } = false;
        #endregion
    }
}
