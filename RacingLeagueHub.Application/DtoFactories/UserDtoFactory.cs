using RacingLeagueHub.Application.Dtos.User;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.BLL.Entities;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.DtoFactories;

public class UserDtoFactory : DtoFactoryBase<User, UserDto>
{
    public override bool FromDto(User entity, UserDto dto)
    {
        entity.Username = dto.Username;
        entity.DriverId = dto.DriverId?.RawId;

        return true;
    }

    public override Expression<Func<User, UserDto>> ToDtoExpression()
    {
        return user => new UserDto
        {
            Username = user.Username,
            DriverId = user.DriverId != null ? new EncryptedId(user.DriverId.Value) : null
        };
    }
}
