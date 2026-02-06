using F1StatsServer.Data;
using F1StatsServer.Infrastructure;
using F1StatsServer.Entities;

namespace F1StatsServer.Repositories
{
    public class CountryRepository : GenericRepository<Country>, IGenericRepository<Country>
    {
        public CountryRepository(AdventureContext context) : base(context)
        {
        }
    }
}
