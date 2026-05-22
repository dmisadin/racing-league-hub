using RacingLeagueHub.Application.Dtos.GrandPrix;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Entities.GrandsPrix;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.DtoMappers;

public class GrandPrixDtoFactory : DtoMapperBase<GrandPrix, GrandPrixDto>
{
    public override bool FromDto(GrandPrix entity, GrandPrixDto dto)
    {
        entity.SeasonId = dto.SeasonId.RawId;
        entity.TrackLayoutId = dto.TrackLayoutId.RawId;
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
            Id = new EncryptedId(gp.Id),
            TrackLayoutId = new EncryptedId(gp.TrackLayoutId),
            LeagueId = new EncryptedId(gp.Season.LeagueId),
            SeasonId = new EncryptedId(gp.SeasonId),
            Name = gp.Name,
            StartingAt = gp.StartingAt,
            VodUrl = gp.VodUrl,
            Slug = gp.Slug
        };
    }
}
