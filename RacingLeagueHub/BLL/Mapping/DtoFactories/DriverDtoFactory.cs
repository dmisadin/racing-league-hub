using F1StatsServer.BLL.Models.Dtos;
using F1StatsServer.Entities;
using System.Linq.Expressions;

namespace F1StatsServer.BLL.Mapping.DtoFactories;

public class DriverDtoFactory : DtoFactoryBase<Driver, DriverDto>
{
    public override void FromDto(Driver entity, DriverDto dto)
    {
        entity.Nickname = dto.Nickname;
        entity.FirstName = dto.FirstName;
        entity.LastName = dto.LastName;
        entity.Country = dto.Country;
        entity.Slug = string.IsNullOrEmpty(dto.Slug) ? dto.Nickname : dto.Slug;
    }

    public override Expression<Func<Driver, DriverDto>> ToDtoExpression()
    {
        return driver => new DriverDto
        {
            Nickname = driver.Nickname,
            FirstName = driver.FirstName,
            LastName = driver.LastName,
            Country = driver.Country,
            Slug = driver.Slug
        };
    }
}
