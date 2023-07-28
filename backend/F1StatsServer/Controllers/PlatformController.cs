using F1StatsServer.Dto;
using F1StatsServer.Infrastructure;
using F1StatsServer.Model;

namespace F1StatsServer.Controllers
{
    public class PlatformController : GenericController<Platform, PlatformDto>
    {
        public PlatformController(IGenericRepository<Platform> genericRepository) : base(genericRepository)
        {
        }
    }
}
