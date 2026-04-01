using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Interceptors.EntityHandlers;

namespace RacingLeagueHub.Infrastructure;

public partial class AdventureContext : DbContext
{
    private readonly IEnumerable<IEntityHandler> handlers;

    public AdventureContext(DbContextOptions<AdventureContext> options,
            IEnumerable<IEntityHandler> handlers) 
        : base(options) 
    {
        this.handlers = handlers;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyDbMapsFromAssembly(typeof(AdventureContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        var entries = CaptureChanges();

        // --- BEFORE hooks + Validate ---
        foreach (var (entry, state, original) in entries)
        {
            var handlers = GetHandlers(entry.GetType());
            foreach (var h in handlers)
            {
                h.Validate(entry);
                if (state == EntityState.Added) h.BeforeAdded(entry);
                if (state == EntityState.Modified) h.BeforeUpdate(entry, original!, this);
                if (state == EntityState.Deleted) h.BeforeDeleted(entry);
            }
        }

        var result = await base.SaveChangesAsync(ct);

        // --- AFTER hooks ---
        foreach (var (entry, state, original) in entries)
        {
            var handlers = GetHandlers(entry.GetType());
            foreach (var h in handlers)
            {
                if (state == EntityState.Added) h.AfterAdded(entry);
                if (state == EntityState.Modified) h.AfterUpdate(entry, original!, this);
                if (state == EntityState.Deleted) h.AfterDeleted(entry);
            }
        }

        return result;
    }

    // Snapshot all pending changes BEFORE SaveChanges detaches/clears them
    private List<(IEntity Entity, EntityState State, IEntity? Original)> CaptureChanges()
    {
        var result = new List<(IEntity, EntityState, IEntity?)>();

        foreach (var entry in ChangeTracker.Entries<IEntity>()
                     .Where(e => e.State is EntityState.Added
                                          or EntityState.Modified
                                          or EntityState.Deleted))
        {
            IEntity? original = null;

            if (entry.State == EntityState.Modified)
            {
                // Build a shallow copy of original values
                original = (IEntity)Activator.CreateInstance(entry.Entity.GetType())!;
                foreach (var prop in entry.Properties)
                {
                    var pi = entry.Entity.GetType().GetProperty(prop.Metadata.Name);
                    pi?.SetValue(original, prop.OriginalValue);
                }
            }

            result.Add((entry.Entity, entry.State, original));
        }

        return result;
    }

    private IEnumerable<IEntityHandler> GetHandlers(Type entityType) =>
        this.handlers.Where(h => h.CanHandle(entityType));
}
