using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.TrackLayouts.Dtos;

namespace RacingLeagueHub.Application.TrackLayouts.Persistence;

public interface ITrackLayoutQueries
{
    Task<TrackLayoutDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<PagedResult<TrackLayoutDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct);
}
