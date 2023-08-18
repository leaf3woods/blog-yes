using BlogYes.Domain.Entities.Base;
using BlogYes.Domain.ValueObjects.UserValue;

namespace BlogYes.Domain.Entities
{
    public class User : UniversalEntity, ISoftDelete
    {
        public string Username { get; set; } = null!;
        public string Passphrase { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string TelephoneNumber { get; set; } = null!;
        public DateTime RegisterTime { get; set; }
        public Setting? Settings { get; set; }
        public Detail? Detail { get; set; }

        #region navigation

        public virtual ICollection<Blog>? Blogs { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;

        #endregion navigation

        #region delete filter

        public bool SoftDeleted { get; set; } = false;
        public DateTime? DeleteTime { get; set; } = null;

        #endregion delete filter

        public readonly static User DevUser = new()
        {
            Username = "dev",
            Passphrase = "Uh+8E9ft9jptdMzAVRKo0UYQtqn5epsbJUZQGbL/Xhk=",
            Salt = "5+fPPv0FShtKo3ed746TiuNojEZsxuPkhbU+YvF5DuQ=",
            DisplayName = "developer",
            Email = "unknow",
            TelephoneNumber = "unknow",
            RegisterTime = DateTime.UnixEpoch,
            RoleId = Role.DevRole.Id,
        };

        public static User[] Seeds { get; } = new User[]
        {
            DevUser
        };
    }
}