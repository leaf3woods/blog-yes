using BlogYes.Infrastructure.Models.Base;

namespace BlogYes.Infrastructure.Models
{
    public class UserDo : UniversalDo, ISoftDelete
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string TelephoneNumber { get; set; } = null!;
        public int Gender { get; set; }
        public string? AboutMe { get; set; }
        public ICollection<BlogDo> Blogs { get; set; } = new List<BlogDo>();

        #region filter
        public bool SoftDeleted { get; set; } = false;
        #endregion
    }
}