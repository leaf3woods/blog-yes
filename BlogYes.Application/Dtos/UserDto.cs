using BlogYes.Application.Dtos.Base;
using BlogYes.Domain.Entities;
using BlogYes.Domain.ValueObjects.User;

namespace BlogYes.Application.Dtos
{
    public class UserRegisterDto : CreateDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string TelephoneNumber { get; set; } = null!;
        public Guid RoleId { get; set; }
        public DateTime RegisterTime { get; set; }
    }

    public class UserReadDto : UpdateDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string TelephoneNumber { get; set; } = null!;
        public Role Role { get; set; } = null!;
        public DateTime RegisterTime { get; set; }
        public Setting? Settings { get; set; }
        public Detail? Detail { get; set; }
    }

    public class UserLoginDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
