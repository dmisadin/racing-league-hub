using F1StatsServer.Dto.DriverDtos;
using F1StatsServer.Dto.ResultsDtos;
using F1StatsServer.Infrastructure;
using F1StatsServer.Model;

namespace F1StatsServer.Controllers
{
    public class RaceController : GenericController<Race, RaceDto>
    {
        public RaceController(IGenericRepository<Race> genericRepository) : base(genericRepository)
        {
        }
    }
}
