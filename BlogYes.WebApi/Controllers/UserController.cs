using BlogYes.Application.Auth;
using BlogYes.Application.Dtos;
using BlogYes.Application.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogYes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = ManagedResource.User)]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(
            IUserService userService
            )
        {
            _userService = userService;
        }

        private readonly IUserService _userService;

        [HttpGet]
        [Authorize]
        [Route("id/{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = $"{ManagedResource.User}.{ManagedAction.Read}.{ManagedItem.Id}")]
        public async Task<ActionResult<UserReadDto>> GetUser(Guid userId) =>
            Ok(await _userService.GetUserAsync(userId));

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = $"{ManagedResource.User}.{ManagedAction.Read}.{ManagedItem.All}")]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetUsers() =>
            Ok(await _userService.GetUsersAsync());

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string> Login(UserLoginDto credential) =>
            await _userService.LoginAsync(credential);

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<int> Register(UserRegisterDto registerDto) =>
            await _userService.RegisterAsync(registerDto);

        [HttpDelete]
        [Route("id/{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = $"{ManagedResource.User}.{ManagedAction.Delete}.{ManagedItem.Id}")]
        public async Task<int> Delete(Guid userId) =>
            await _userService.DeleteAsync(userId);
    }
}
