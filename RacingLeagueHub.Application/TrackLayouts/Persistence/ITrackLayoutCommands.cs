using RacingLeagueHub.Application.TrackLayouts.Dtos;

namespace RacingLeagueHub.Application.TrackLayouts.Persistence;

public interface ITrackLayoutCommands
{
    Task<TrackLayoutDto> AddAsync(CreateTrackLayoutDto dto, CancellationToken ct);
    Task<TrackLayoutDto?> UpdateAsync(long id, UpdateTrackLayoutDto dto, CancellationToken ct);
}
