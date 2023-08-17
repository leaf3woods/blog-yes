using BlogYes.Application.Auth;
using BlogYes.Application.Dtos;
using BlogYes.Application.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogYes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = ManagedResource.Role)]
    public class RoleController : ControllerBase
    {
        public RoleController(
            IRoleService roleService
            )
        {
            _roleService = roleService;
        }

        private readonly IRoleService _roleService;

        [HttpGet]
        [Route("id/{roleId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = $"{ManagedResource.Role}.{ManagedAction.Read}.{ManagedItem.Id}")]
        public async Task<ActionResult<RoleReadDto?>> GetRole(Guid roleId) =>
            Ok(await _roleService.GetRoleAsync(roleId));

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = $"{ManagedResource.Role}.{ManagedAction.Read}.{ManagedItem.All}")]
        public async Task<ActionResult<IEnumerable<RoleReadDto>>> GetRoles() =>
            Ok(await _roleService.GetRolesAsync());

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = $"{ManagedResource.Role}.{ManagedAction.Create}.{ManagedItem.Dto}")]
        public async Task<ActionResult<RoleReadDto?>> CreateRole(RoleCreateDto roleDto) =>
            Ok(await _roleService.CreateRoleAsync(roleDto));

        [HttpPut]
        [Route("id/{roleId:guid}/scopes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = $"{ManagedResource.Role}.{ManagedAction.Update}.{"Scopes"}")]
        public async Task<ActionResult<int>> ModifyRoleScopeAsync(Guid roleId, List<string> scopes) =>
            Ok(await _roleService.ModifyRoleScopeAsync(roleId, scopes));

        [HttpGet]
        [Route("scopes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = $"{ManagedResource.Role}.{ManagedAction.Read}.{"Scopes"}")]
        public ActionResult<IEnumerable<RoleScopeReadDto>> GetSupportedScopes() => Ok(_roleService.GetScopes());
    }
}