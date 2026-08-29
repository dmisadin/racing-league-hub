using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Services.TrackService.Dtos;
using RacingLeagueHub.Application.Services.TrackService.Persistence;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.Persistence.Teams;

internal class TrackCommands : ITrackCommands
{
    private readonly RacingContext racingContext;
    private readonly IDtoMapper<Track, TrackDto> mapper;

    public TrackCommands(
        RacingContext racingContext,
        IDtoMapper<Track, TrackDto> mapper)
    {
        this.racingContext = racingContext;
        this.mapper = mapper;
    }

    public async Task<TrackDto> AddAsync(CreateTrackDto dto, CancellationToken ct)
    {
        var track = new Track
        {
            Name = dto.Name
        };

        racingContext.Track.Add(track);

        await racingContext.SaveChangesAsync(ct);

        return mapper.ToDto(track);
    }

    public async Task<TrackDto?> UpdateAsync(long id, UpdateTrackDto dto, CancellationToken ct)
    {
        var track = await racingContext.Track
            .SingleOrDefaultAsync(
                t => t.Id == id,
                ct);

        if (track is null)
            return null;

        track.Name = dto.Name;

        await racingContext.SaveChangesAsync(ct);

        return mapper.ToDto(track);
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken ct)
    {
        var track = await racingContext.Track
            .SingleOrDefaultAsync(
                t => t.Id == id,
                ct);

        if (track is null)
            return false;

        racingContext.Track.Remove(track);

        await racingContext.SaveChangesAsync(ct);

        return true;
    }
}
