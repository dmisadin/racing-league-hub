using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Services.ResourceService.Dtos;

namespace RacingLeagueHub.Application.Services.ResourceService.Persistence;

public interface IResourceQueries
{
    Task<ResourceDto?> GetByIdAsync(long id, CancellationToken ct);
    Task<PagedResult<ResourceDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct);
    Task<string?> GetFileUrlAsync(long id, CancellationToken ct);
}
