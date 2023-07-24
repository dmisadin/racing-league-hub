using F1StatsServer.Dto;
using F1StatsServer.Infrastructure;
using F1StatsServer.Model;

namespace F1StatsServer.Controllers
{
    public class RegionController : GenericController<Region, RegionDto>
    {
        public RegionController(IGenericRepository<Region> genericRepository) : base(genericRepository)
        {
        }
    }
}
