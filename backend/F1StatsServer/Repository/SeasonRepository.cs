using F1StatsServer.Data;
using F1StatsServer.Interface;
using F1StatsServer.Model;

namespace F1StatsServer.Repository
{
    public class SeasonRepository : IGenericRepository<Season>
    {
        private readonly AdventureContext _context;
        public SeasonRepository(AdventureContext context)
        {
            _context = context;
        }

        public List<Season> Get()
        {
            return _context.Seasons.OrderBy(o => o.PkSeasonId).ToList();
        }

        public Season GetById(int id)
        {
            return _context.Seasons.Where(c => c.PkSeasonId == id).FirstOrDefault();
        }
    }
}
