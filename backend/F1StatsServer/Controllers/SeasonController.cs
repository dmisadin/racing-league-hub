using F1StatsServer.Dto.SeasonDtos;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interface;
using F1StatsServer.Model;
using Microsoft.AspNetCore.Mvc;

namespace F1StatsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonController : GenericController<Season, SeasonDto>
    {
        private readonly ISeasonService _seasonService;
        public SeasonController(IGenericRepository<Season> genericRepository, ISeasonService seasonService) : base(genericRepository)
        {
            _seasonService = seasonService;
        }

        [HttpPost("create")]
        public IActionResult InsertSeason(SeasonInsertDto data)
        {
            _seasonService.InsertSeason(data);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }
    }
}
