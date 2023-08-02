using F1StatsServer.Dto.GrandPrixDtos;
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

        public GrandPrixController(
            IGenericRepository<GrandPrix> genericRepository,
            IGrandPrixRepository grandPrixRepository,
            IGrandPrixService grandPrixService
            ) : base(genericRepository)
        {
            _genericRepository = genericRepository;
            _grandPrixRepository = grandPrixRepository;
            _grandPrixService = grandPrixService;
        }

        [HttpGet("homepage")]
        [ProducesResponseType(200)]
        public IActionResult GetHomepageData()
        {
            var grandPrix = _grandPrixRepository.GetData();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(grandPrix);

        }

        [HttpGet("display/{id}")]
        [ProducesResponseType(200)]
        public IActionResult GetPageData(int id)
        {
            if (!_genericRepository.Has(id))
                return NotFound();

            var grandPrix = _grandPrixRepository.GetTrackData(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(grandPrix);
        }

        [HttpPost("create")]
        [ProducesResponseType(200)]
        public IActionResult InsertData(List<GrandPrixInsertDto> data)
        {
            if(data == null)
                return BadRequest(ModelState);

            var result = _grandPrixService.InsertData(data);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result);
        }
    }
}
