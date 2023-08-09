using F1StatsServer.Dto.LeagueDtos;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interface;
using F1StatsServer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> InsertLeague(LeagueInsertDto data)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _leagueService.InsertLeagueAsync(data));
        }

        [HttpGet("display")]
        public async Task<IActionResult> GetLeagues()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _leagueService.GetLeaguesAsync();

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("display/{id}")]
        public async Task<IActionResult> GetLeagueData(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _leagueService.GetLeagueDataAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
