using F1StatsServer.Dto.UserDto;

namespace F1StatsServer.Interfaces
{
    public interface IUserService
    {
        string GetMyName();
        string Login(UserDto request);
    }
}
