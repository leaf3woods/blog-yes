﻿using BlogYes.Application.Auth.Requirements;
using BlogYes.Application.Services.Base;
using BlogYes.Core;
using BlogYes.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace BlogYes.Application.Auth.AuthHandler
{
    public class RoleRequireHandler : AuthorizationHandler<RoleRequireScope>
    {
        public RoleRequireHandler(
            IUserService userService,
            IRoleService roleService,
            ILogger logger)
        {
            _userService = userService;
            _roleService = roleService;
            _logger = logger;
        }

        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly ILogger _logger;

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequireScope requirement)
        {
            var dict = context.User.Claims.ToDictionary(key => key.Type, value => value.Value);
            if(!dict.TryGetValue(CustomClaimsType.UserId, out var uId) ||
                !dict.TryGetValue(CustomClaimsType.RoleId, out var rId))
            {
                _logger.LogWarning("unknow token claims");
                context.Fail();
                return;
            }
            var userId = Guid.Parse(uId);
            var roleId = Guid.Parse(rId);

            if (roleId == Role.DevRole.Id || roleId == Role.SuperRole.Id)
            {
                context.Succeed(requirement);
                return;
            }

            var role = await _roleService.GetRoleAsync(roleId);
            if(role is null)
            {
                _logger.LogWarning("a token with unknow role");
                context.Fail();
                return;
            }
            var user = await _userService.GetUserAsync(userId);
            if (user is null)
            {
                _logger.LogWarning("a token with unknow user");
                context.Fail();
                return;
            }

            if (role.Scopes.Any(s => requirement.Scope.Contains(s.Name)))
            {
                _logger.LogInformation("scope match success");
                context.Succeed(requirement);
                return;
            }
            context.Fail();
        }
    }
}