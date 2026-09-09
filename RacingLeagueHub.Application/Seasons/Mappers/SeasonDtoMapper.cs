using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Seasons.Dtos;
using RacingLeagueHub.Domain.Entities.Seasons;
using RacingLeagueHub.Domain.Services.Interfaces;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.Seasons.Mappers;

public class SeasonDtoMapper(IStorageService storageService) 
    : DtoMapperBase<Season, SeasonDto>
{
    public override bool FromDto(Season entity, SeasonDto dto)
    {
        entity.LeagueId = dto.LeagueId.RawId;
        entity.Name = dto.Name;
        entity.Platform = dto.Platform;
        entity.Game = dto.Game;
        entity.LapPercentageRequired = dto.LapPercentageRequired;
        entity.Slug = dto.Slug;
        entity.LogoResourceId = dto.LogoResourceId?.RawId;

        return true;
    }

    public override Expression<Func<Season, SeasonDto>> ToDtoExpression()
    {
        var baseStorageUrl = storageService.GetBaseUrl();

        return season => new SeasonDto
        {
            Id = new EncryptedId(season.Id),
            LeagueId = new EncryptedId(season.LeagueId),
            Name = season.Name,
            Platform = season.Platform,
            Game = season.Game,
            LapPercentageRequired = season.LapPercentageRequired,
            Slug = season.Slug,
            LogoResourceId = season.LogoResourceId != null ? new EncryptedId(season.LogoResourceId.Value) : null,
            LogoUrl = season.LogoResourceId == null
                        ? null
                        : baseStorageUrl + "/uploads/" + season.LogoResource.StorageId + "." + season.LogoResource.Extension

        };
    }
}