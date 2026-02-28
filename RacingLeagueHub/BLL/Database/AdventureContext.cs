using Microsoft.EntityFrameworkCore;

namespace RacingLeagueHub.BLL.Database;

public partial class AdventureContext : DbContext
{
    public AdventureContext(DbContextOptions<AdventureContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyDbMapsFromAssembly(typeof(AdventureContext).Assembly);
    }
}
