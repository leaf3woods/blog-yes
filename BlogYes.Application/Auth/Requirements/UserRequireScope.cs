using BlogYes.Domain.ValueObjects.UserValue;
using Microsoft.AspNetCore.Authorization;

namespace BlogYes.Application.Auth.Requirements
{
    public class UserRequireScope : IAuthorizationRequirement
    {
        public Scope Scope { get; set; } = null!; 
    }
}
