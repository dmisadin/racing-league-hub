using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Domain.Resources;

namespace RacingLeagueHub.Infrastructure.Persistence.EntityHandlers.Resources;

public class EntityHandlerResourceHelper(DbContext db)
{
    public void MarkActive(int id)
    {
        var resource = GetOrThrow(id);
        resource.Status = ResourceStatus.Active;
    }

    public void MarkForDelete(int id)
    {
        var resource = GetOrThrow(id);
        resource.Status = ResourceStatus.MarkedForDeletion;
    }

    private Resource GetOrThrow(int id)
    {
        var resource = db.Set<Resource>().Find(id);
        if (resource is null)
            throw new KeyNotFoundException($"Resource {id} not found.");
        return resource;
    }
}