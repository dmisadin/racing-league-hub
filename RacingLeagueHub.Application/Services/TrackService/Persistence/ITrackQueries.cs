using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Services.TrackService.Dtos;

namespace RacingLeagueHub.Application.Services.TrackService.Persistence;

public interface ITrackQueries
{
    Task<TrackDto?> GetByIdAsync(long id, CancellationToken ct);
    Task<PagedResult<TrackDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct);
}
