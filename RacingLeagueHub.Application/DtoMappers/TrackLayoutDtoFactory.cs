using RacingLeagueHub.Application.Dtos.Track;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Entities;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.DtoMappers;

public class TrackLayoutDtoFactory : DtoMapperBase<TrackLayout, TrackLayoutDto>
{
    public override bool FromDto(TrackLayout entity, TrackLayoutDto dto)
    {
        entity.TrackId = dto.TrackId.RawId;
        entity.Name = dto.Name;
        entity.PitStopDuration = dto.PitStopDuration;
        entity.CornersTotal = dto.CornersTotal;
        entity.CornersLeft = dto.CornersLeft;
        entity.LapsGrandPrix = dto.LapsGrandPrix;
        entity.Length = dto.Length;
        entity.ElevationChange = dto.ElevationChange;
        entity.TelemetryId = dto.TelemetryId;

        return true;
    }

    public override Expression<Func<TrackLayout, TrackLayoutDto>> ToDtoExpression()
    {
        return tl => new TrackLayoutDto
        {
            Id = new EncryptedId(tl.Id),
            TrackId = new EncryptedId(tl.TrackId),
            Name = tl.Name,
            PitStopDuration = tl.PitStopDuration,
            CornersTotal = tl.CornersTotal,
            CornersLeft = tl.CornersLeft,
            LapsGrandPrix = tl.LapsGrandPrix,
            Length = tl.Length,
            ElevationChange = tl.ElevationChange,
            TelemetryId = tl.TelemetryId,
            TrackLayoutGames = tl.TrackLayoutGames.Select(game => game.Game).ToList()
        };
    }
}
