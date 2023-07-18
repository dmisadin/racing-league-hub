using F1StatsServer.Data;
using F1StatsServer.Infrastructure;
using F1StatsServer.Model;

namespace F1StatsServer.Repository
{
    public class RegionRepository : GenericRepository<Region>, IGenericRepository<Region>
    {
    public RegionRepository(AdventureContext context) : base(context)
    {
    }
}
}
