using F1StatsServer.Dto.GrandPrixDto;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interface;
using F1StatsServer.Model;
using F1StatsServer.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F1StatsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrandPrixController : GenericController<GrandPrix, GrandPrixDto>
    {
        public IGenericRepository<GrandPrix> _genericRepository;
        public IGrandPrixRepository _grandPrixRepository;

        public GrandPrixController(
            IGenericRepository<GrandPrix> genericRepository,
            IGrandPrixRepository grandPrixRepository
            ) : base(genericRepository)
        {
            _genericRepository = genericRepository;
            _grandPrixRepository = grandPrixRepository;
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

        [HttpGet("page/{id}")]
        [ProducesResponseType(200)]
        public IActionResult GetPageData(int id)
        {
            var grandPrix = _grandPrixRepository.GetTrackData(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(grandPrix);
        }
    }
}
