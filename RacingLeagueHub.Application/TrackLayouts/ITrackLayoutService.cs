using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.TrackLayouts.Dtos;

namespace RacingLeagueHub.Application.TrackLayouts;

public interface ITrackLayoutService
{
    Task<TrackLayoutDto?> GetByIdAsync(long id, CancellationToken ct);
    Task<PagedResult<TrackLayoutDto>> GetPagedAsync(int page, CancellationToken ct);
    Task<TrackLayoutDto> AddAsync(CreateTrackLayoutDto dto, CancellationToken ct);
    Task<TrackLayoutDto?> UpdateAsync(long id, UpdateTrackLayoutDto dto, CancellationToken ct);
}
