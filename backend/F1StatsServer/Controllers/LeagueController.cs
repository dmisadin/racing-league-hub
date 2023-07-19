using F1StatsServer.Dto;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interface;
using F1StatsServer.Model;
using Microsoft.AspNetCore.Mvc;

namespace F1StatsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeagueController : GenericController<League, LeagueDto>
    {
        private readonly ILeagueService _leagueService;

        public LeagueController(IGenericRepository<League> genericRepository, ILeagueService leagueService) : base(genericRepository)
        {
            _leagueService = leagueService;
        }

        [HttpPost("create")]
        public IActionResult InsertLeague(LeagueInsertDto data)
        {
            _leagueService.InsertLeague(data);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }
    }
}
