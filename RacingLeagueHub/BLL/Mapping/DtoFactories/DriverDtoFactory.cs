using RacingLeagueHub.BLL.Entities;
using System.Linq.Expressions;
using RacingLeagueHub.API.Dtos;

namespace RacingLeagueHub.BLL.Mapping.DtoFactories;

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
            Id = driver.Id,
            Nickname = driver.Nickname,
            FirstName = driver.FirstName,
            LastName = driver.LastName,
            Country = driver.Country,
            Slug = driver.Slug
        };
    }
}
