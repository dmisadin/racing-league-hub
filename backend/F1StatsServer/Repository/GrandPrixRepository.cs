using F1StatsServer.Data;
using F1StatsServer.Interface;
using F1StatsServer.Model;

namespace F1StatsServer.Repository
{
    public class GrandPrixRepository : GenericRepository<GrandPrix>, IGenericRepository<GrandPrix>
    {
        public GrandPrixRepository(AdventureContext context) : base(context)
        {
        }
    }
}
