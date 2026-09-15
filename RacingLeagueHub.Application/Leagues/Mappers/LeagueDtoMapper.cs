using RacingLeagueHub.Application.Common.Mappers;
using RacingLeagueHub.Application.Leagues.Dtos;
using RacingLeagueHub.Application.Resources;
using RacingLeagueHub.Domain.Leagues;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.Leagues.Mappers;

public class LeagueDtoMapper(IStorageService storageService) 
    : DtoMapperBase<League, LeagueDto>
{
    public override bool FromDto(League entity, LeagueDto dto)
    {
        entity.Name = dto.Name;
        entity.Abbreviation = dto.Abbreviation;
        entity.Description = dto.Description;
        entity.Region = dto.Region;
        entity.Timezone = dto.Timezone;
        entity.Slug = dto.Slug;
        entity.LogoResourceId = dto.LogoResourceId;

        return true;
    }

    public override Expression<Func<League, LeagueDto>> ToDtoExpression()
    {
        var baseStorageUrl = storageService.GetBaseUrl();

        return league => new LeagueDto
        {
            Id = league.Id,
            Name = league.Name,
            Abbreviation = league.Abbreviation,
            Region = league.Region,
            Description = league.Description,
            Timezone = league.Timezone,
            Slug = league.Slug,
            LogoResourceId = league.LogoResourceId,
            LogoUrl = league.LogoResourceId == null 
                ? null 
                : baseStorageUrl + "/uploads/" + league.LogoResource!.StorageId + "." + league.LogoResource.Extension
        };
    }
}
