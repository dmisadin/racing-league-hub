using F1StatsServer.Dto;
using F1StatsServer.Model;

namespace F1StatsServer.Interface
{
    public interface IUserRepository
    {
        bool CheckCredentials(string email, string password);
        bool RegisterUser(RegisterDto data);
        User CheckRole(string email, string password);
    }
}
