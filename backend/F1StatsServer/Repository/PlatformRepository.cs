using F1StatsServer.Data;
using F1StatsServer.Infrastructure;
using F1StatsServer.Model;

namespace F1StatsServer.Repository
{
    public class PlatformRepository : GenericRepository<Region>, IGenericRepository<Region>
    {
    public PlatformRepository(AdventureContext context) : base(context)
    {
    }
}
}
