using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Tracks.Dtos;

namespace RacingLeagueHub.Application.Tracks;

public interface ITrackService
{
    Task<TrackDto?> GetByIdAsync(long id, CancellationToken ct);
    Task<PagedResult<TrackDto>> GetPagedAsync(int page, CancellationToken ct);
    Task<TrackDto> AddAsync(CreateTrackDto dto, CancellationToken ct);
    Task<TrackDto?> UpdateAsync(long id, UpdateTrackDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(long id, CancellationToken ct);
}
