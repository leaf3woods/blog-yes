using BlogYes.Application.Dtos.Base;
using BlogYes.Domain.ValueObjects.UserValue;

namespace BlogYes.Application.Dtos
{
    public class UserRegisterDto : CreateDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string TelephoneNumber { get; set; } = null!;
        public DateTime RegisterTime { get; set; }
    }

    public class UserReadDto : ReadDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string TelephoneNumber { get; set; } = null!;
        public RoleReadDto Role { get; set; } = null!;
        public DateTime RegisterTime { get; set; }
        public UserSettingReadDto? Settings { get; set; }
        public UserDetailReadDto? Detail { get; set; }
    }

    public class UserLoginDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class UserSettingReadDto : ReadDto
    {
        public string Language { get; set; } = "Chinese";
    }

    public class UserDetailReadDto : ReadDto
    {
        public Gender Gender { get; set; } = Gender.Unknow;
        public string? AboutMe { get; set; }
    }
}