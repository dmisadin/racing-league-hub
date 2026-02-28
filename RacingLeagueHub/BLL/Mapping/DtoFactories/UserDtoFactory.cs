using RacingLeagueHub.BLL.Models.Dtos.User;
using RacingLeagueHub.Entities;
using System.Linq.Expressions;

namespace RacingLeagueHub.BLL.Mapping.DtoFactories;

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
