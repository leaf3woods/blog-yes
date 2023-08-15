using BlogYes.Application.Dtos.Base;

namespace BlogYes.Application.Dtos
{
    public class UserCreateDto : CreateDto
    {

    }

    public class UserUpdateDto : UpdateDto
    {

    }

    public class UserReadDto : UpdateDto
    {

    }

    public class UserLoginDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
