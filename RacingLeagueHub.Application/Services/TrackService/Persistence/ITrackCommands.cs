using RacingLeagueHub.Application.Services.TrackService.Dtos;

namespace RacingLeagueHub.Application.Services.TrackService.Persistence;

public interface ITrackCommands
{
    Task<TrackDto> AddAsync(CreateTrackDto dto, CancellationToken ct);
    Task<TrackDto?> UpdateAsync(long id, UpdateTrackDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(long id, CancellationToken ct);
}
