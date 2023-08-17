
using BlogYes.Domain.ValueObjects.UserValue;
using Microsoft.AspNetCore.Authorization;

namespace BlogYes.Application.Auth.Requirements
{
    public class DefaultRequireScope : IAuthorizationRequirement
    {
        public Scope Scope { get; set; } = new Scope {
            Name = "Default",
            Description = "role with default scope can access any api",
        };
    }
}
