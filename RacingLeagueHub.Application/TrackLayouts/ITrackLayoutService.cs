using RacingLeagueHub.Application.TrackLayouts.Dtos;
using RacingLeagueHub.Domain.Common.Models;

namespace RacingLeagueHub.Application.TrackLayouts;

public interface ITrackLayoutService
{
    Task<TrackLayoutDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<PagedResult<TrackLayoutDto>> GetPagedAsync(int page, CancellationToken ct);
    Task<TrackLayoutDto> AddAsync(CreateTrackLayoutDto dto, CancellationToken ct);
    Task<TrackLayoutDto?> UpdateAsync(int id, UpdateTrackLayoutDto dto, CancellationToken ct);
}
