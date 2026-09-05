using RacingLeagueHub.Application.Tracks.Dtos;

namespace RacingLeagueHub.Application.Tracks.Persistence;

public interface ITrackCommands
{
    Task<TrackDto> AddAsync(CreateTrackDto dto, CancellationToken ct);
    Task<TrackDto?> UpdateAsync(long id, UpdateTrackDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(long id, CancellationToken ct);
}
