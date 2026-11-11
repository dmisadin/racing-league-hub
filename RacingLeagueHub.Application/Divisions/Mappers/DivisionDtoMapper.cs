using RacingLeagueHub.Application.Common.Mappers;
using RacingLeagueHub.Application.Divisions.Dtos;
using RacingLeagueHub.Domain.Leagues.Divisions;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.Divisions.Mappers;

public class DivisionDtoMapper : DtoMapperBase<Division, DivisionDto>
{
    public override bool FromDto(Division entity, DivisionDto dto)
    {
        entity.LeagueId = dto.LeagueId;
        entity.Name = dto.Name;
        entity.Slug = dto.Slug;
        entity.Tier = dto.Tier;

        return true;
    }

    public override Expression<Func<Division, DivisionDto>> ToDtoExpression()
    {
        return division => new DivisionDto
        {
            Id = division.Id,
            LeagueId = division.LeagueId,
            Name = division.Name,
            Slug = division.Slug,
            Tier = division.Tier
        };
    }
}
