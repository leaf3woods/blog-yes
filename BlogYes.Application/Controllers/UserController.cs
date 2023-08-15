using AutoMapper;
using BlogYes.Application.Dtos;
using BlogYes.Application.Utilities;
using BlogYes.Core;
using BlogYes.Domain.Entities;
using BlogYes.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogYes.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(
            IUserRepository userRepository, 
            IMapper mapper,
            ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        [HttpGet]
        [Route("id/{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserReadDto>> GetUser(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            var result = _mapper.Map<UserReadDto>(user);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();
            var results = _mapper.Map<IEnumerable<UserReadDto>>(users);
            return Ok(results);
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<string> Login(UserLoginDto credential)
        {
            var user = await _userRepository.FindAsync(credential.Username);
            if(user is null || user.Password != credential.Password)
            {
                throw new Exception("user not exist or password error");
            }
            var token = EncryptUtil.GenerateJwtToken(SettingUtil.Jwt.Issuer, SettingUtil.Jwt.Audience, SettingUtil.Jwt.ExpireMin,
                new Claim(CustomClaimsType.UserId, user.Id.ToString()), new Claim(CustomClaimsType.Role, user.Role.ToString()!)) ??
                throw new Exception("generate jwt token error");
            return token;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<int> Register(UserCreateDto registerDto)
        {
            var user = _mapper.Map<User>(registerDto);
            return await _userRepository.CreateAsync(user);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<int> Update(UserUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("id/{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<int> Delete(Guid id)
        {
            return await _userRepository.DeleteAsync(id);
        }
    }
}
