using F1StatsServer.Data;
using F1StatsServer.Interface;
using F1StatsServer.Model;

namespace F1StatsServer.Repository
{
    public class RaceRepository : IGenericRepository<Race>
    {
        private readonly AdventureContext _context;
        public RaceRepository(AdventureContext context)
        {
            _context = context;
        }

        public List<Race> Get()
        {
            return _context.Races.OrderBy(o => o.Position).ToList();
        }

        public Race GetById(int id)
        {
            return _context.Races.Where(c => c.FkRaceDriverId == id).FirstOrDefault();
        }

        public bool Has(int id)
        {
            return _context.Races.Any(c => c.FkRaceDriverId == id);
        }
    }
}
