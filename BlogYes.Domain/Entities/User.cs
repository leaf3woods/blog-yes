using BlogYes.Domain.Entities.Base;

namespace BlogYes.Domain.Entities
{
    public class User : UniversalEntity
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string TelephoneNumber { get; set; } = null!;
        public Gender Gender { get; set; }
        public string? AboutMe { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}