using F1StatsServer.Dto.UserDto;
using F1StatsServer.Entities;

namespace F1StatsServer.Interfaces
{
    public interface IUserRepository
    {
        bool CheckCredentials(string email, string password);
        bool RegisterUser(RegisterDto data);
        User CheckRole(string email, string password);
    }
}
