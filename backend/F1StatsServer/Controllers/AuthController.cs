using F1StatsServer.Interface;
using F1StatsServer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using F1StatsServer.Dto.UserDto;

namespace F1StatsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public AuthController(IUserRepository userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetMe()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = _userService.GetMyName();

            return Ok(username);
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.CheckCredentials(request.Email, request.Password))
            {
                return BadRequest("User not found.");
            }

            string token = _userService.Login(request);

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
