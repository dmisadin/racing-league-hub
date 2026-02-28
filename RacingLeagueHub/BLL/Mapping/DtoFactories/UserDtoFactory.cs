using F1StatsServer.BLL.Models.Dtos.User;
using F1StatsServer.Entities;
using System.Linq.Expressions;

namespace F1StatsServer.BLL.Mapping.DtoFactories;

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
