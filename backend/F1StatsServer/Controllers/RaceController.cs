using F1StatsServer.Dto.DriverDtos;
using F1StatsServer.Dto.ResultsDtos;
using F1StatsServer.Infrastructure;
using F1StatsServer.Entities;

namespace F1StatsServer.Controllers
{
    public class RaceController : GenericController<SessionResult, RaceResultDto>
    {
        public RaceController(IGenericRepository<SessionResult> genericRepository) : base(genericRepository)
        {
        }
    }
}
