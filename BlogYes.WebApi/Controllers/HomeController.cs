﻿using BlogYes.Application.Dtos;
using BlogYes.Application.Services.Base;
using BlogYes.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUserService _userService;

        /// <summary>
        ///     获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("captcha")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponseWrapper<CaptchaReadDto?>>> GetCaptcha() =>
            (await _userService.GenerateCaptchaAsync()).Wrap();

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
        public async Task<ResponseWrapper<string>> Login(UserLoginDto credential) =>
            ResponseWrapper<string>.Create(await _userService.LoginAsync(credential));

        /// <summary>
        ///     用户登出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("logout")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Logout() => await _userService.LogoutAsync();

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
        public async Task<ResponseWrapper<UserReadDto?>> Register(UserRegisterDto registerDto) =>
            (await _userService.RegisterAsync(registerDto)).Wrap();
    }
}