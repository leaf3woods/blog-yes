using BlogYes.Application.Captchas;
using BlogYes.Application.Dtos;
using BlogYes.Application.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace BlogYes.WebApi.Controllers
{
    /// <summary>
    ///     主页
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public HomeController(
                IUserService userService
            )
        {
            _userService = userService;
        }
        private IUserService _userService;

        [HttpGet]
        [Route("captcha")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CaptchaReadDto>> GetCaptcha() => await _userService.GenerateCaptchaAsync();
    }
}