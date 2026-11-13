using RacingLeagueHub.Application.Common.Mappers;
using RacingLeagueHub.Application.GrandsPrix.Dtos;
using RacingLeagueHub.Domain.GrandsPrix;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.GrandsPrix.Mappers;

public class GrandPrixDtoMapper : DtoMapperBase<GrandPrix, GrandPrixDto>
{
    public override bool FromDto(GrandPrix entity, GrandPrixDto dto)
    {
        entity.SeasonDivisionId = dto.SeasonDivisionId;
        entity.TrackLayoutId = dto.TrackLayoutId;
        entity.Name = dto.Name;
        entity.StartingAt = dto.StartingAt;
        entity.VodUrl = dto.VodUrl;
        entity.Slug = dto.Slug;

        return true;
    }

    /// <remarks>Reads navigations, so use it in queries rather than on freshly created entities.</remarks>
    public override Expression<Func<GrandPrix, GrandPrixDto>> ToDtoExpression()
    {
        return gp => new GrandPrixDto
        {
            Id = gp.Id,
            TrackLayoutId = gp.TrackLayoutId,
            LeagueId = gp.SeasonDivision.Season.LeagueId,
            SeasonId = gp.SeasonDivision.SeasonId,
            SeasonDivisionId = gp.SeasonDivisionId,
            DivisionSlug = gp.SeasonDivision.Division.Slug,
            DivisionName = gp.SeasonDivision.Division.Name,
            Name = gp.Name,
            StartingAt = gp.StartingAt,
            VodUrl = gp.VodUrl,
            Slug = gp.Slug,
            TrackName = gp.TrackLayout.Track.Name,
            TrackCity = gp.TrackLayout.Track.City,
            TrackCountryCode = gp.TrackLayout.Track.Country.CodeAlpha2
        };
    }
}
