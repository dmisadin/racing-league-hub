using F1StatsServer.Dto.DriverDto;
using F1StatsServer.Infrastructure;
using F1StatsServer.Model;

namespace F1StatsServer.Controllers
{
    public class RaceController : GenericController<Race, DriverDto>
    {
        public RaceController(IGenericRepository<Race> genericRepository) : base(genericRepository)
        {
        }
    }
}
