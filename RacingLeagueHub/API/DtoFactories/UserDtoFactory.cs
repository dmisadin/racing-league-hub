using RacingLeagueHub.BLL.Entities;
using System.Linq.Expressions;
using RacingLeagueHub.API.Dtos.User;

namespace RacingLeagueHub.API.DtoFactories;

public class UserDtoFactory : DtoFactoryBase<User, UserDto>
{
    public override void FromDto(User entity, UserDto dto)
    {
        entity.Username = dto.Username;
        entity.DriverId = dto.DriverId;
    }

    public override Expression<Func<User, UserDto>> ToDtoExpression()
    {
        return user => new UserDto
        {
            Username = user.Username,
            DriverId = user.DriverId
        };
    }
}
