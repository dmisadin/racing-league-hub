using RacingLeagueHub.Application.TrackLayouts.Dtos;

namespace RacingLeagueHub.Application.TrackLayouts.Persistence;

public interface ITrackLayoutCommands
{
    Task<TrackLayoutDto> AddAsync(CreateTrackLayoutDto dto, CancellationToken ct);
    Task<TrackLayoutDto?> UpdateAsync(int id, UpdateTrackLayoutDto dto, CancellationToken ct);
}
