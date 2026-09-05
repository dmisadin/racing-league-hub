using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Services.TrackService.Dtos;
using RacingLeagueHub.Application.TrackLayouts.Dtos;
using RacingLeagueHub.Domain.Entities;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.DtoMappers;

public class TrackDtoMapper : DtoMapperBase<Track, TrackDto>
{
    public override bool FromDto(Track entity, TrackDto dto)
    {
        entity.Name = dto.Name;
        // entity.Country = dto.CountryAlpha2; stop using this
        entity.City = dto.City;
        entity.ShortName = dto.ShortName;

        return true;
    }

    public override Expression<Func<Track, TrackDto>> ToDtoExpression()
    {
        return track => new TrackDto
        {
            Id = new EncryptedId(track.Id),
            Name = track.Name,
            Country = new CountryDto(track.Country.Id, track.Country.CodeAlpha2, track.Country.Name),
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
