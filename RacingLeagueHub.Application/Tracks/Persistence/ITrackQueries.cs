using RacingLeagueHub.Application.Common.Dtos;
using RacingLeagueHub.Application.Tracks.Dtos;
using RacingLeagueHub.Domain.Common.Models;

namespace RacingLeagueHub.Application.Tracks.Persistence;

public interface ITrackQueries
{
    Task<TrackDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<PagedResult<TrackDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct);
    Task<List<LookupDto>> GetLookupsAsync(CancellationToken ct);
}
