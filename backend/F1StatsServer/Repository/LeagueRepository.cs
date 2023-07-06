using F1StatsServer.Data;
using F1StatsServer.Interface;
using F1StatsServer.Model;

namespace F1StatsServer.Repository
{
    public class LeagueRepository : GenericRepository<League>, IGenericRepository<League>
    {
        public LeagueRepository(AdventureContext context) : base(context)
        {
        }
    }
}
