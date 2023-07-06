using F1StatsServer.Dto;
using F1StatsServer.Interface;
using F1StatsServer.Model;
using Microsoft.AspNetCore.Mvc;

namespace F1StatsServer.Controllers
{
    public class SeasonController : GenericController<Season, DriverDto>
    {
        public SeasonController(IGenericRepository<Season> genericRepository) : base(genericRepository)
        {
        }
    }
}
