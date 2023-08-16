using BlogYes.Application.Dtos;
using BlogYes.Application.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace BlogYes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(
            IUserService userService, 
            ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        [HttpGet]
        [Route("id/{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserReadDto>> GetUser(Guid userId) =>
            Ok(await _userService.GetUserAsync(userId));

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetUsers() =>
            Ok(await _userService.GetUsersAsync());

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<string> Login(UserLoginDto credential) =>
            await _userService.LoginAsync(credential);

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<int> Register(UserRegisterDto registerDto) =>
            await _userService.RegisterAsync(registerDto);

        [HttpDelete]
        [Route("id/{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<int> Delete(Guid id) =>
            await _userService.DeleteAsync(id);
    }
}
