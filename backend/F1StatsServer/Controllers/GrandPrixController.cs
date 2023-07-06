using F1StatsServer.Dto;
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
        public IGenericRepository<Season> _seasonRepository;
        public IGenericRepository<GrandPrix> _genericRepository;
        public IGenericRepository<League> _leagueRepository;

        public GrandPrixController(
            IGenericRepository<GrandPrix> genericRepository,
            IGenericRepository<Season> seasonRepository,
            IGenericRepository<League> leagueRepository
            ) : base(genericRepository)
        {
            _seasonRepository = seasonRepository;
            _genericRepository = genericRepository;
            _leagueRepository = leagueRepository;
        }

        [HttpGet("homepage")]
        [ProducesResponseType(200)]
        public IActionResult GetHomepageData()
        {
            var grandPrix = _genericRepository.Get();
            List<GrandPrixHomeDto> grandPrixMapped = new List<GrandPrixHomeDto>();

            foreach (var item in grandPrix)
            {
                var season = _seasonRepository.GetById(item.FkGrandPrixSeasonId);
                var league = _leagueRepository.GetById(season.FkSeasonLeagueId);
                grandPrixMapped.Add(new GrandPrixHomeDto { 
                    Id = item.PkGrandPrixId, 
                    GrandPrixName = item.Name,
                    SeasonName = season.Name,
                    LeagueName = league.Name,
                });
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(grandPrixMapped);

        }
    }
}
