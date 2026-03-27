using RacingLeagueHub.API.Dtos.Track;
using RacingLeagueHub.BLL.Entities;
using System.Linq.Expressions;

namespace RacingLeagueHub.BLL.Mapping.DtoFactories.Admin;

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
            Id = track.Id,
            Name = track.Name,
            CountryAlpha2 = track.Country,
            City = track.City,
            ShortName = track.ShortName,
            TrackLayouts = track.TrackLayouts.Select(tl => new TrackLayoutDto
            {
                Id = tl.Id,
                TrackId = tl.TrackId,
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
