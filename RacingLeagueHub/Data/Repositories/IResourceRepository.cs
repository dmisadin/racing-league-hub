using RacingLeagueHub.BLL.Entities.Resources;

namespace RacingLeagueHub.Data.Repositories;

public interface IResourceRepository
{
    Task<Resource?> GetOneAsync(long id, CancellationToken ct = default);
    Task<Resource?> GetOneAsync(Guid uid, CancellationToken ct = default);
    Task<IReadOnlyList<Resource>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Resource>> GetPendingOlderThanAsync(DateTimeOffset cutoff, CancellationToken ct = default);
    Task<Resource> CreateAsync(Resource resource, CancellationToken ct = default);
    Task ConfirmAsync(Resource resource, CancellationToken ct = default);
    Task DeleteAsync(Resource resource, CancellationToken ct = default);
}
