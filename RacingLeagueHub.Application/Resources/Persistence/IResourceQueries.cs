using RacingLeagueHub.Application.Resources.Dtos;
using RacingLeagueHub.Domain.Common.Models;
using RacingLeagueHub.Domain.Resources;

namespace RacingLeagueHub.Application.Resources.Persistence;

public interface IResourceQueries
{
    Task<ResourceDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<PagedResult<ResourceDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct);
    Task<string?> GetFileUrlAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<Resource>> GetPendingOlderThanAsync(DateTimeOffset cutoff, CancellationToken ct = default);
}
