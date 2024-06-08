using F1StatsServer.Dto.SeasonDtos;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interfaces;
using F1StatsServer.Entities;
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
        public async Task<IActionResult> InsertSeason(SeasonInsertDto data)
        {
            await _seasonService.InsertSeasonAsync(data);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }

        [HttpGet("display/{id}")]
        [ProducesResponseType(200, Type = typeof(SeasonDisplayDto))]
        public async Task<IActionResult> GetSeasonData(int id)
        {
            var result = await _seasonService.GetSeasonDataAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("SessionPoints/{id}")]
        [ProducesResponseType(200, Type = typeof(SeasonSessionPointsDto))]
        public async Task<IActionResult> GetSeasonSessionPoints(int id)
        {
            var result = await _seasonService.GetSeasonSessionPointsAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("GetAssists/{id}")]
        public async Task<IActionResult> GetSeasonAssists (int id)
        {
            var result = await _seasonService.GetAssistsAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("GetLobbySettings/{id}")]
        public async Task<IActionResult> GetSeasonLobbySettings(int id)
        {
            var result = await _seasonService.GetLobbySettingsAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet(nameof(GetSeasonDrivers))]
        public async Task<IActionResult> GetSeasonDrivers(int seasonId)
        {
            var result = await _seasonService.GetSeasonDrivers(seasonId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /* to be implemented with new Entity GameTeams, move it to TeamService
        [HttpGet(nameof(GetSeasonTeams))]
        public async Task<IActionResult> GetSeasonTeams(int seasonId)
        {
            return
        }
        */
    }
}
