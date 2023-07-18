using F1StatsServer.Data;
using F1StatsServer.Infrastructure;
using F1StatsServer.Model;

namespace F1StatsServer.Repository
{
    public class RaceRepository : GenericRepository<Race>, IGenericRepository<Race>
    {
        public RaceRepository(AdventureContext context) : base(context)
        {
        }
    }
}
