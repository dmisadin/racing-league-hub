using F1StatsServer.Data;
using F1StatsServer.Interface;
using F1StatsServer.Model;

namespace F1StatsServer.Repository
{
    public class CountryRepository : IGenericRepository<Country>
    {
        private readonly AdventureContext _context;

        public CountryRepository(AdventureContext context)
        {
            _context = context;
        }
        public List<Country> Get()
        {
            return _context.Countries.OrderBy(o => o.PkCountryId).ToList();
        }

        public Country GetById(int id)
        {
            return _context.Countries.Where(c => c.PkCountryId == id).FirstOrDefault();
        }
    }
}
