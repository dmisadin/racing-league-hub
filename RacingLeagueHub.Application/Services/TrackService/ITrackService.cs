using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Services.TeamService.Dtos;
using RacingLeagueHub.Application.Services.TrackService.Dtos;

namespace RacingLeagueHub.Application.Services.TrackService;

public interface ITrackService
{
    Task<TrackDto?> GetByIdAsync(long id, CancellationToken ct);
    Task<PagedResult<TrackDto>> GetPagedAsync(int page, CancellationToken ct);
    Task<TrackDto> AddAsync(CreateTrackDto dto, CancellationToken ct);
    Task<TrackDto?> UpdateAsync(long id, UpdateTrackDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(long id, CancellationToken ct);
}
