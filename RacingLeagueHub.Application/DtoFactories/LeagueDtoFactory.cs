using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Application.Dtos.Resource;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Storage;
using RacingLeagueHub.Domain.Entities;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.DtoFactories;

public class LeagueDtoFactory : DtoMapperBase<League, LeagueDto>
{
    public override bool FromDto(League entity, LeagueDto dto)
    {
        entity.Name = dto.Name;
        entity.Abbreviation = dto.Abbreviation;
        entity.Description = dto.Description;
        entity.Region = dto.Region;
        entity.Timezone = dto.Timezone;
        entity.Slug = dto.Slug;
        entity.LogoResourceId = dto.LogoResourceId?.RawId;

        return true;
    }

    public override Expression<Func<League, LeagueDto>> ToDtoExpression()
    {
        var baseStorageUrl = S3Settings.Values.PublicBaseUrl;

        return league => new LeagueDto
        {
            Id = new EncryptedId(league.Id),
            Name = league.Name,
            Abbreviation = league.Abbreviation,
            Region = league.Region,
            Description = league.Description,
            Timezone = league.Timezone,
            Slug = league.Slug,
            LogoResourceId = league.LogoResourceId != null ? new EncryptedId(league.LogoResourceId.Value) : null,
            LogoUrl = league.LogoResourceId == null 
                ? null 
                : baseStorageUrl + "/uploads/" + league.LogoResource!.StorageId + "." + league.LogoResource.Extension
        };
    }
}
