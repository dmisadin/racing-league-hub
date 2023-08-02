using F1StatsServer.Dto.DriverDtos;
using F1StatsServer.Infrastructure;
using F1StatsServer.Model;

namespace F1StatsServer.Controllers
{
    public class DriverController : GenericController<Driver, DriverDto>
    {
        public DriverController(IGenericRepository<Driver> genericRepository) : base(genericRepository)
        {

        }
    }
}
