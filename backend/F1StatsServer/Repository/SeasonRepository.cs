using F1StatsServer.Data;
using F1StatsServer.Infrastructure;
using F1StatsServer.Model;

namespace F1StatsServer.Repository
{
    public class SeasonRepository : GenericRepository<Season>, IGenericRepository<Season>
    {
        public SeasonRepository(AdventureContext context) : base(context)
        {
        }
    }
}
