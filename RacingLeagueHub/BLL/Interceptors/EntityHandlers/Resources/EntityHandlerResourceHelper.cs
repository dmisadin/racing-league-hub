using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.BLL.Entities.Resources;

namespace RacingLeagueHub.BLL.Interceptors.EntityHandlers.Resources;

public class EntityHandlerResourceHelper(DbContext db)
{
    public void MarkActive(long id)
    {
        var resource = GetOrThrow(id);
        resource.Status = ResourceStatus.Active;
    }

    public void MarkForDelete(long id)
    {
        var resource = GetOrThrow(id);
        resource.Status = ResourceStatus.MarkedForDeletion;
    }

    private Resource GetOrThrow(long id)
    {
        var resource = db.Set<Resource>().Find(id);
        if (resource is null)
            throw new KeyNotFoundException($"Resource {id} not found.");
        return resource;
    }
}