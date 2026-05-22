using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Entities;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.DtoMappers;

public class DriverDtoMapper : DtoMapperBase<Driver, DriverDto>
{
    public override bool FromDto(Driver entity, DriverDto dto)
    {
        entity.Nickname = dto.Nickname;
        entity.FirstName = dto.FirstName;
        entity.LastName = dto.LastName;
        entity.Country = dto.Country;
        entity.Slug = string.IsNullOrEmpty(dto.Slug) ? dto.Nickname : dto.Slug;

        return true;
    }

    public override Expression<Func<Driver, DriverDto>> ToDtoExpression()
    {
        return driver => new DriverDto
        {
            Id = new EncryptedId(driver.Id),
            Nickname = driver.Nickname,
            FirstName = driver.FirstName,
            LastName = driver.LastName,
            Country = driver.Country,
            Slug = driver.Slug
        };
    }
}
