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
    }
}
