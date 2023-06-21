using F1StatsServer.Dto;

namespace F1StatsServer.Interface
{
    public interface IUserRepository
    {
        bool CheckCredentials(string name,string password);
        bool RegisterUser(RegisterDto data);
    }
}
