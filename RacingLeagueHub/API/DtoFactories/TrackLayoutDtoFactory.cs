using RacingLeagueHub.API.Dtos.Track;
using RacingLeagueHub.BLL.Entities;
using RacingLeagueHub.BLL.Models;
using System.Linq.Expressions;

namespace RacingLeagueHub.API.DtoFactories;

public class TrackLayoutDtoFactory : DtoFactoryBase<TrackLayout, TrackLayoutDto>
{
    public override void FromDto(TrackLayout entity, TrackLayoutDto dto)
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

        entity.TrackLayoutGames.Where(tlg => !dto.TrackLayoutGames.Contains(tlg.Game))
                                .ToList()
                                .ForEach(tlg => entity.TrackLayoutGames.Remove(tlg));

        var newGames = dto.TrackLayoutGames.Where(g => entity.TrackLayoutGames.All(tlg => tlg.Game != g));

        foreach (var game in newGames)
        {
            entity.TrackLayoutGames.Add(new TrackLayoutGame { Game = game });
        }
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
