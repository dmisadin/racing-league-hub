using F1StatsServer.Data;
using F1StatsServer.Interface;
using F1StatsServer.Model;

namespace F1StatsServer.Repository
{
    public class GrandPrixRepository : IGenericRepository<GrandPrix>
    {
        private readonly AdventureContext _context;
        public GrandPrixRepository(AdventureContext context)
        {
            _context = context;
        }
        public List<GrandPrix> Get()
        {
            return _context.GrandPrixes.OrderBy(o => o.PkGrandPrixId).ToList();
        }

        public GrandPrix GetById(int id)
        {
            return _context.GrandPrixes.Where(c => c.PkGrandPrixId == id).FirstOrDefault();
        }

        public bool Has(int id)
        {
            return _context.GrandPrixes.Any(c => c.PkGrandPrixId == id);
        }

    }
}
