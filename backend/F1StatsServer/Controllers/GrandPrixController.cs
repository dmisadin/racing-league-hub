using F1StatsServer.Dto.GrandPrixDtos;
using F1StatsServer.Dto.ResultsDtos;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interfaces;
using F1StatsServer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace F1StatsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrandPrixController : GenericController<GrandPrix, GrandPrixDto>
    {
        public IGenericRepository<GrandPrix> _genericRepository;
        public IGrandPrixRepository _grandPrixRepository;
        private readonly IGrandPrixService _grandPrixService;
        private readonly ISessionResultService _sessionResultService;

        public GrandPrixController(
            IGenericRepository<GrandPrix> genericRepository,
            IGrandPrixRepository grandPrixRepository,
            IGrandPrixService grandPrixService,
            ISessionResultService sessionResultService) : base(genericRepository)
        {
            _genericRepository = genericRepository;
            _grandPrixRepository = grandPrixRepository;
            _grandPrixService = grandPrixService;
            _sessionResultService = sessionResultService;
        }

        [HttpGet("homepage")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetHomepageData()
        {
            var grandPrix = await _grandPrixRepository.GetDataAsync();

            return Ok(grandPrix);

        }

        [HttpGet("display/{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetGrandPrixData(int id)
        {
            if (! _genericRepository.Has(id))
                return NotFound();

            var grandPrix = await _grandPrixRepository.GetGrandPrixDataAsync(id);

            return Ok(grandPrix);
        }

        [HttpPost("create")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> InsertData(List<GrandPrixInsertDto> data)
        {
            if(data == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            var result = await _grandPrixService.InsertDataAsync(data);

            return Ok(result);
        }

        [HttpPost(nameof(InsertResults))]
        [ProducesResponseType(200)]
        public async Task<IActionResult> InsertResults(List<SessionResultDto> data)
        {
            if(data == null)
                return BadRequest(ModelState);

            var result = await _sessionResultService.InsertResultsAsync(data);

            if (!ModelState.IsValid || !result)
                return BadRequest(ModelState);

            return Ok(result);
        }

        [HttpGet("startingsoon")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetGrandPrixStartingSoon()
        {
            var grandPrix = await _grandPrixRepository.GetGrandPrixStartingSoon();

            return Ok(grandPrix);

        }
        [HttpGet("live")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetGrandPrixLive()
        {
            var grandPrix = await _grandPrixRepository.GetGrandPrixLive();

            return Ok(grandPrix);

        }
    }
}
