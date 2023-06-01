using F1StatsServer.Data;
using F1StatsServer.Interface;
using F1StatsServer.Model;

namespace F1StatsServer.Repository
{
    public class LeagueRepository : IGenericRepository<League>
    {
        private readonly AdventureContext _context;
        public LeagueRepository(AdventureContext context)
        {
            _context = context;
        }

        public List<League> Get()
        {
            return _context.Leagues.OrderBy(o => o.PkLeagueId).ToList();
        }

        public League GetById(int id)
        {
            return _context.Leagues.Where(c => c.PkLeagueId == id).FirstOrDefault();
        }
    }
}
