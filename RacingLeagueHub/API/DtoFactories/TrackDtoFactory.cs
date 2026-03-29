using RacingLeagueHub.API.Dtos.Track;
using RacingLeagueHub.BLL.Entities;
using RacingLeagueHub.BLL.Models;
using System.Linq.Expressions;

namespace RacingLeagueHub.API.DtoFactories;

public class TrackDtoFactory : DtoFactoryBase<Track, TrackDto>
{
    public override void FromDto(Track entity, TrackDto dto)
    {
        entity.Name = dto.Name;
        entity.Country = dto.CountryAlpha2;
        entity.City = dto.City;
        entity.ShortName = dto.ShortName;
    }

    public override Expression<Func<Track, TrackDto>> ToDtoExpression()
    {
        return track => new TrackDto
        {
            Id = new EncryptedId(track.Id),
            Name = track.Name,
            CountryAlpha2 = track.Country,
            City = track.City,
            ShortName = track.ShortName,
            TrackLayouts = track.TrackLayouts.Select(tl => new TrackLayoutDto
            {
                Id = new EncryptedId(tl.Id),
                TrackId = new EncryptedId(tl.TrackId),
                Name = tl.Name,
                PitStopDuration = tl.PitStopDuration,
                CornersTotal = tl.CornersTotal,
                CornersLeft = tl.CornersLeft,
                LapsGrandPrix = tl.LapsGrandPrix,
                Length = tl.Length,
                TelemetryId = tl.TelemetryId,
                ElevationChange = tl.ElevationChange,
                TrackLayoutGames = tl.TrackLayoutGames.Select(game => game.Game).ToList()
            }).ToList()
        };
    }
}
