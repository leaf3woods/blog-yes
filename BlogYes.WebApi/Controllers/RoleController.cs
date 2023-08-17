using BlogYes.Application.Dtos;
using BlogYes.Application.Services.Base;
using BlogYes.Domain.ValueObjects.UserValue;
using Microsoft.AspNetCore.Mvc;

namespace BlogYes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<RoleReadDto?>> GetRole(Guid roleId) =>
            Ok(await _roleService.GetRoleAsync(roleId));

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RoleReadDto>>> GetRoles() =>
            Ok(await _roleService.GetRolesAsync());

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoleReadDto?>> CreateRole(RoleCreateDto roleDto) =>
            Ok(await _roleService.CreateRoleAsync(roleDto));

        [HttpPut]
        [Route("id/{roleId:guid}/scopes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> ModifyRoleScopeAsync(Guid roleId, List<RoleScopeModifyDto> scopes) =>
            Ok(await _roleService.ModifyRoleScopeAsync(roleId, scopes));

        [HttpGet]
        [Route("scopes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<RoleScopeReadDto>> GetSupportedScopes() => Ok(_roleService.GetScopes());

    }
}
