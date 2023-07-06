using F1StatsServer.Data;
using F1StatsServer.Interface;
using F1StatsServer.Model;

namespace F1StatsServer.Repository
{
    public class DriverRepository : GenericRepository<Driver>, IGenericRepository<Driver>
    {
        public DriverRepository(AdventureContext context) : base(context)
        {
        }
    }
}
