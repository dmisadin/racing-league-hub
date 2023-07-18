using F1StatsServer.Data;
using F1StatsServer.Infrastructure;
using F1StatsServer.Model;

namespace F1StatsServer.Repository
{
    public class CountryRepository : GenericRepository<Country>, IGenericRepository<Country>
    {
        public CountryRepository(AdventureContext context) : base(context)
        {
        }
    }
}
