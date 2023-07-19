using F1StatsServer.Dto.UserDto;

namespace F1StatsServer.Interface
{
    public interface IUserService
    {
        string GetMyName();
        string Login(UserDto request);
    }
}
