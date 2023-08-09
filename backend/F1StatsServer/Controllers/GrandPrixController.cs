using F1StatsServer.Dto.GrandPrixDtos;
using F1StatsServer.Dto.ResultsDtos;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interface;
using F1StatsServer.Model;
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
        private readonly IResultService _resultService;

        public GrandPrixController(
            IGenericRepository<GrandPrix> genericRepository,
            IGrandPrixRepository grandPrixRepository,
            IGrandPrixService grandPrixService,
            IResultService resultService) : base(genericRepository)
        {
            _genericRepository = genericRepository;
            _grandPrixRepository = grandPrixRepository;
            _grandPrixService = grandPrixService;
            _resultService = resultService;
        }

        [HttpGet("homepage")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetHomepageData()
        {
            var grandPrix = await _grandPrixRepository.GetDataAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(grandPrix);

        }

        [HttpGet("display/{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetGrandPrixData(int id)
        {
            if (! _genericRepository.Has(id))
                return NotFound();

            var grandPrix = await _grandPrixRepository.GetGrandPrixDataAsync(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(grandPrix);
        }

        [HttpPost("create")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> InsertData(List<GrandPrixInsertDto> data)
        {
            if(data == null)
                return BadRequest(ModelState);

            var result = await _grandPrixService.InsertDataAsync(data);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result);
        }

        [HttpPost("create/{id}/results")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> InsertResults(ResultInsertDto data,int id)
        {
            if(data == null)
                return BadRequest(ModelState);

            var result = await _resultService.InsertResultsAsync(data, id);

            if (ModelState.IsValid || result == -1)
                return BadRequest(ModelState);

            return Ok(result);
        }
    }
}
