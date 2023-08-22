using BlogYes.Application.Auth;
using BlogYes.Application.Dtos;
using BlogYes.Application.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogYes.WebApi.Controllers
{
    /// <summary>
    ///     角色资源
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = ManagedResource.Role)]
    public class RoleController : ControllerBase
    {
        /// <summary>
        ///     注入
        /// </summary>
        /// <param name="roleService"></param>
        public RoleController(
            IRoleService roleService
            )
        {
            _roleService = roleService;
        }

        private readonly IRoleService _roleService;

        /// <summary>
        ///     获取角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{roleId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = $"{ManagedResource.Role}.{ManagedAction.Read}.{ManagedItem.Id}")]
        public async Task<ActionResult<RoleReadDto?>> GetRole(Guid roleId) =>
            Ok(await _roleService.GetRoleAsync(roleId));

        /// <summary>
        ///     获取所有角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = $"{ManagedResource.Role}.{ManagedAction.Read}.{ManagedItem.All}")]
        public async Task<ActionResult<IEnumerable<RoleReadDto>>> GetRoles() =>
            Ok(await _roleService.GetRolesAsync());

        /// <summary>
        ///     创建新角色
        /// </summary>
        /// <param name="roleDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = $"{ManagedResource.Role}.{ManagedAction.Create}.{ManagedItem.Dto}")]
        public async Task<ActionResult<RoleReadDto?>> CreateRole(RoleCreateDto roleDto) =>
            Ok(await _roleService.CreateRoleAsync(roleDto));

        /// <summary>
        ///     编辑角色权限范围
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="scopes"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{roleId:guid}/scopes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = $"{ManagedResource.Role}.{ManagedAction.Update}.{"Scopes"}")]
        public async Task<ActionResult<int>> ModifyRoleScopeAsync(Guid roleId, List<string> scopes) =>
            Ok(await _roleService.ModifyRoleScopeAsync(roleId, scopes));

        /// <summary>
        ///     获取支持的权限范围
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("scopes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = $"{ManagedResource.Role}.{ManagedAction.Read}.{"Scopes"}")]
        public ActionResult<IEnumerable<RoleScopeReadDto>> GetSupportedScopes() => Ok(_roleService.GetScopes());
    }
}