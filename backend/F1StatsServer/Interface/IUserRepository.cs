using F1StatsServer.Dto;

namespace F1StatsServer.Interface
{
    public interface IUserRepository
    {
        bool CheckCredentials(string email,string password);
        bool RegisterUser(RegisterDto data);
    }
}
