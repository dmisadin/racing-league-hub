using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Storage;
using RacingLeagueHub.Domain.Entities.Seasons;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.DtoFactories;

public class SeasonDtoFactory : DtoFactoryBase<Season, SeasonDto>
{
    public override bool FromDto(Season entity, SeasonDto dto)
    {
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
        var baseStorageUrl = S3Settings.Values.PublicBaseUrl;

        return season => new SeasonDto
        {
            Id = new EncryptedId(season.Id),
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