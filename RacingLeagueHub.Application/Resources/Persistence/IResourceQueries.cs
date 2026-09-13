using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Resources.Dtos;
using RacingLeagueHub.Domain.Entities.Resources;

namespace RacingLeagueHub.Application.Resources.Persistence;

public interface IResourceQueries
{
    Task<ResourceDto?> GetByIdAsync(long id, CancellationToken ct);
    Task<PagedResult<ResourceDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct);
    Task<string?> GetFileUrlAsync(long id, CancellationToken ct);
    Task<IReadOnlyList<Resource>> GetPendingOlderThanAsync(DateTimeOffset cutoff, CancellationToken ct = default);
}
