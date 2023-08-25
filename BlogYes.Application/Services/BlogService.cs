using BlogYes.Application.Auth;
using BlogYes.Application.Services.Base;
using BlogYes.Domain.Utilities;

namespace BlogYes.Application.Services
{
    [Scope("ploicy to use blog", ManagedResource.Blog)]
    public class BlogService : BaseService, IBlogService
    {
    }
}