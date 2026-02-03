using F1StatsServer.Data;
using F1StatsServer.Infrastructure;
using F1StatsServer.Entities;

namespace F1StatsServer.Repositories
{
    public class RaceRepository : GenericRepository<SessionResult>, IGenericRepository<SessionResult>
    {
        public RaceRepository(AdventureContext context) : base(context)
        {
        }
    }
}
