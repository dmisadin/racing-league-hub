using RacingLeagueHub.BLL.Entities;
using RacingLeagueHub.BLL.Models.Dtos.Track;
using System.Linq.Expressions;

namespace RacingLeagueHub.BLL.Mapping.DtoFactories.Admin;

public class TrackDtoFactory : DtoFactoryBase<Track, TrackDto>
{
    public override void FromDto(Track entity, TrackDto dto)
    {
        entity.Name = dto.Name;
        entity.Country = dto.CountryAlpha2;
        entity.City = dto.City;
        entity.Elevation = dto.Elevation;
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
            Elevation = track.Elevation,
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
                TrackLayoutGames = tl.TrackLayoutGames.Select(game => game.Game).ToList()
            }).ToList()
        };
    }
}
