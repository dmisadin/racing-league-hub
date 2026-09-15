using RacingLeagueHub.Application.Common.Mappers;
using RacingLeagueHub.Application.GrandsPrix.Dtos;
using RacingLeagueHub.Domain.GrandsPrix;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.GrandsPrix.Mappers;

public class GrandPrixDtoMapper : DtoMapperBase<GrandPrix, GrandPrixDto>
{
    public override bool FromDto(GrandPrix entity, GrandPrixDto dto)
    {
        entity.SeasonId = dto.SeasonId;
        entity.TrackLayoutId = dto.TrackLayoutId;
        entity.Name = dto.Name;
        entity.StartingAt = dto.StartingAt;
        entity.VodUrl = dto.VodUrl;
        entity.Slug = dto.Slug;

        return true;
    }

    public override Expression<Func<GrandPrix, GrandPrixDto>> ToDtoExpression()
    {
        return gp => new GrandPrixDto
        {
            Id = gp.Id,
            TrackLayoutId = gp.TrackLayoutId,
            LeagueId = gp.Season.LeagueId,
            SeasonId = gp.SeasonId,
            Name = gp.Name,
            StartingAt = gp.StartingAt,
            VodUrl = gp.VodUrl,
            Slug = gp.Slug
        };
    }
}
