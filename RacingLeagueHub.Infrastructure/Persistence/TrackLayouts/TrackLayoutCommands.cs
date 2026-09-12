using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.TrackLayouts.Dtos;
using RacingLeagueHub.Application.TrackLayouts.Persistence;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Models.Enums;

namespace RacingLeagueHub.Infrastructure.Persistence.TrackLayouts;

internal class TrackLayoutCommands : ITrackLayoutCommands
{
    private readonly RacingContext racingContext;
    private readonly IDtoMapper<TrackLayout, TrackLayoutDto> mapper;

    public TrackLayoutCommands(RacingContext racingContext,
        IDtoMapper<TrackLayout, TrackLayoutDto> mapper)
    {
        this.racingContext = racingContext;
        this.mapper = mapper;
    }

    public async Task<TrackLayoutDto> AddAsync(CreateTrackLayoutDto dto, CancellationToken ct)
    {
        var layout = new TrackLayout
        {
            TrackId = dto.TrackId.RawId,
            Name = dto.Name,
            PitStopDuration = dto.PitStopDuration,
            CornersTotal = dto.CornersTotal,
            CornersLeft = dto.CornersLeft,
            LapsGrandPrix = dto.LapsGrandPrix,
            ElevationChange = dto.ElevationChange,
            Length = dto.Length,
            TelemetryId = dto.TelemetryId,
            MapImageResourceId = dto.MapImageResourceId?.RawId,
            CoverImageResourceId = dto.CoverImageResourceId?.RawId
        };

        foreach (var game in dto.TrackLayoutGames.Distinct())
        {
            layout.TrackLayoutGames.Add(new TrackLayoutGame
            {
                Game = game
            });
        }

        racingContext.TrackLayout.Add(layout);

        await racingContext.SaveChangesAsync(ct);

        return mapper.ToDto(layout);
    }

    public async Task<TrackLayoutDto?> UpdateAsync(long id, UpdateTrackLayoutDto dto, CancellationToken ct)
    {
        var layout = await racingContext.TrackLayout
            .Include(x => x.TrackLayoutGames)
            .SingleOrDefaultAsync(x => x.Id == id, ct);

        if (layout is null)
            return null;

        layout.Name = dto.Name;
        layout.PitStopDuration = dto.PitStopDuration;
        layout.CornersTotal = dto.CornersTotal;
        layout.CornersLeft = dto.CornersLeft;
        layout.LapsGrandPrix = dto.LapsGrandPrix;
        layout.ElevationChange = dto.ElevationChange;
        layout.Length = dto.Length;
        layout.TelemetryId = dto.TelemetryId;
        layout.MapImageResourceId = dto.MapImageResourceId?.RawId;
        layout.CoverImageResourceId = dto.CoverImageResourceId?.RawId;

        UpdateGames(layout, dto.TrackLayoutGames);

        await racingContext.SaveChangesAsync(ct);

        return mapper.ToDto(layout);
    }

    private void UpdateGames(TrackLayout layout, IEnumerable<Game> requestedGames)
    {
        var requested = requestedGames.Distinct().ToHashSet();

        foreach (var existing in layout.TrackLayoutGames.ToList())
        {
            if (!requested.Contains(existing.Game))
            {
                racingContext.TrackLayoutGame.Remove(existing);
            }
        }

        var existingGames = layout.TrackLayoutGames
            .Select(x => x.Game)
            .ToHashSet();

        foreach (var game in requested)
        {
            if (!existingGames.Contains(game))
            {
                layout.TrackLayoutGames.Add(new TrackLayoutGame
                {
                    Game = game
                });
            }
        }
    }
}
