using System.Security.Claims;
using F1StatsServer.Dto.UserDto;
using F1StatsServer.Interface;
using F1StatsServer.Model;

namespace F1StatsServer.Service
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserRepository _userRepository;
        public readonly IConfiguration _configuration;

        public UserService(IHttpContextAccessor contextAccessor, IUserRepository userRepository, IConfiguration configuration)
        {
            _contextAccessor = contextAccessor;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public string GetMyName()
        {
            var result = string.Empty;
            if (_contextAccessor.HttpContext != null)
            {
                result = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }

        public string Login(UserDto request)
        {
            User user = _userRepository.CheckRole(request.Email, request.Password);

            return TokenService.CreateToken(request, _configuration, user.IsAdmin);
        }
    }
}
