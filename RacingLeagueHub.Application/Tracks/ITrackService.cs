using RacingLeagueHub.Application.Common.Dtos;
using RacingLeagueHub.Application.Tracks.Dtos;
using RacingLeagueHub.Domain.Common.Models;

namespace RacingLeagueHub.Application.Tracks;

public interface ITrackService
{
    Task<TrackDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<PagedResult<TrackDto>> GetPagedAsync(int page, CancellationToken ct);
    Task<List<LookupDto>> GetLookupsAsync(CancellationToken ct);
    Task<TrackDto> AddAsync(CreateTrackDto dto, CancellationToken ct);
    Task<TrackDto?> UpdateAsync(int id, UpdateTrackDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
