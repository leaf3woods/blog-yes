using BlogYes.Application.Auth;
using BlogYes.Application.Dtos;
using BlogYes.Application.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogYes.WebApi.Controllers
{
    /// <summary>
    ///     用户资源
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Policy = ManagedResource.User)]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        ///     注入服务
        /// </summary>
        /// <param name="userService">用户服务</param>
        public UserController(
            IUserService userService
            )
        {
            _userService = userService;
        }

        private readonly IUserService _userService;

        /// <summary>
        ///     获取指定Id的用户
        /// </summary>
        /// <param name="userId">GUID</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = $"{ManagedResource.User}.{ManagedAction.Read}.{ManagedItem.Id}")]
        public async Task<ActionResult<UserReadDto>> GetUser(Guid userId) =>
            Ok(await _userService.GetUserAsync(userId));

        /// <summary>
        ///     获取所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = $"{ManagedResource.User}.{ManagedAction.Read}.{ManagedItem.All}")]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetUsers() =>
            Ok(await _userService.GetUsersAsync());

        /// <summary>
        ///     用户登录
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string> Login(UserLoginDto credential) =>
            await _userService.LoginAsync(credential);
        
        /// <summary>
        ///     用户注册
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UserReadDto?> Register(UserRegisterDto registerDto) =>
            await _userService.RegisterAsync(registerDto);

        /// <summary>
        ///     删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = $"{ManagedResource.User}.{ManagedAction.Delete}.{ManagedItem.Id}")]
        public async Task<int> Delete(Guid userId) =>
            await _userService.DeleteAsync(userId);

        /// <summary>
        ///     切换权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{userId:guid}/role/{roleId:guid}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UserReadDto?> ModifyRole(Guid userId, Guid roleId) =>
            await _userService.ChangeRole(userId, roleId);
    }
}