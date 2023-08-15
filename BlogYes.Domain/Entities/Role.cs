using BlogYes.Domain.Entities.Base;

namespace BlogYes.Domain.Entities
{
    public class Role : UniversalEntity, ISoftDelete
    {
        #region navigation
        public virtual ICollection<User>? Users { get; set; }
        #endregion

        #region filter
        public bool SoftDeleted { get; set; } = false;
        #endregion
    }
}
