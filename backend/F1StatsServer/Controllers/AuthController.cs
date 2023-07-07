using F1StatsServer.Dto;
using F1StatsServer.Interface;
using F1StatsServer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using F1StatsServer.Service;

namespace F1StatsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserRepository userRepository, IUserService userService)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetMe()
        {

            var username = _userService.GetMyName();

            return Ok(username);
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            if (!_userRepository.CheckCredentials(request.Email, request.Password))
            {
                return BadRequest("User not found.");
            }

            User user = _userRepository.CheckRole(request.Email, request.Password);

            string token = TokenService.CreateToken(request, _configuration, user.IsAdmin);

            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDto request)
        {
            bool result = _userRepository.RegisterUser(request);

            if (!result | !ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(request);
        }

    }
}
