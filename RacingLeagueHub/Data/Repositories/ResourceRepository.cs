using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.BLL.Entities.Resources;

namespace RacingLeagueHub.Data.Repositories;

public class ResourceRepository(AdventureContext db) : IResourceRepository
{
    private DbSet<Resource> Resources = db.Set<Resource>();

    public IQueryable<Resource> Query()
    {
        return this.Resources;
    }

    public async Task<Resource?> GetOneAsync(long id, CancellationToken ct = default)
    {
        return await Resources.FirstOrDefaultAsync(r => r.Id == id, ct);
    }

    public async Task<Resource?> GetOneAsync(Guid uid, CancellationToken ct = default)
    {
        return await Resources.FirstOrDefaultAsync(r => r.StorageId == uid, ct);
    }

    public async Task<long?> GetIdFromUidAsync(Guid uid, CancellationToken ct = default)
    {
        return await Resources.Where(r => r.StorageId == uid).Select(r => r.Id).FirstOrDefaultAsync(ct);
    }

    public async Task<IReadOnlyList<Resource>> GetAllAsync(CancellationToken ct = default)
    {
        return await Resources.Where(r => r.Status == ResourceStatus.Active)
                            .OrderByDescending(r => r.CreatedAt)
                            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Resource>> GetPendingOlderThanAsync(DateTimeOffset cutoff, CancellationToken ct = default)
    {
        return await Resources.Where(r => r.Status == ResourceStatus.Pending && r.CreatedAt < cutoff)
                            .ToListAsync(ct);
    }

    public async Task<Resource> CreateAsync(Resource resource, CancellationToken ct = default)
    {
        Resources.Add(resource);
        await db.SaveChangesAsync(ct);
        return resource;
    }

    public async Task ConfirmAsync(Resource resource, CancellationToken ct = default)
    {
        resource.Status = ResourceStatus.Active;
        await db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Resource resource, CancellationToken ct = default)
    {
        Resources.Remove(resource);
        await db.SaveChangesAsync(ct);
    }
}
