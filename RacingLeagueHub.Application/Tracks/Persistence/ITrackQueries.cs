using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Tracks.Dtos;

namespace RacingLeagueHub.Application.Tracks.Persistence;

public interface ITrackQueries
{
    Task<TrackDto?> GetByIdAsync(long id, CancellationToken ct);
    Task<PagedResult<TrackDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct);
}
