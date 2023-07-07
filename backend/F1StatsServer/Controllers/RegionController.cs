using F1StatsServer.Dto;
using F1StatsServer.Interface;
using F1StatsServer.Model;
using Microsoft.AspNetCore.Mvc;

namespace F1StatsServer.Controllers
{
    public class RegionController : GenericController<Region, RegionDto>
    {
        public RegionController(IGenericRepository<Region> genericRepository) : base(genericRepository)
        { }
    }
}
