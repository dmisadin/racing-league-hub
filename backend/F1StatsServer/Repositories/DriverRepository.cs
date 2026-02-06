using F1StatsServer.Data;
using F1StatsServer.Infrastructure;
using F1StatsServer.Entities;

namespace F1StatsServer.Repositories
{
    public class DriverRepository : GenericRepository<Driver>, IGenericRepository<Driver>
    {
        public DriverRepository(AdventureContext context) : base(context)
        {
        }
    }
}
