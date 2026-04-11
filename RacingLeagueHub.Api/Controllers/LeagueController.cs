using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Application.DtoFactories;
using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Domain.Abstractions;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Entities.Seasons;

namespace RacingLeagueHub.Api.Controllers;

[Route("api/leagues")]
public class LeagueController : GenericController<League, LeagueDto>
{
    private readonly ILeagueRepository leagueRepository;
    private readonly ISeasonRepository seasonRepository;

    public LeagueController(ILeagueRepository repository, 
                            ISeasonRepository seasonRepository) 
        : base(repository)
    {
        this.leagueRepository = repository;
        this.seasonRepository = seasonRepository;
    }

    protected override IDtoFactory<League, LeagueDto> DtoFactory => new LeagueDtoFactory();
    protected IDtoFactory<Season, SeasonDto> SeasonDtoFactory => new SeasonDtoFactory();

    [HttpGet("{leagueSlug}")]
    public async Task<ActionResult<LeagueDto>> GetBySlug(string leagueSlug)
    {
        if (string.IsNullOrEmpty(leagueSlug))
            return BadRequest();

        var league = await this.leagueRepository.GetBySlugAsync<LeagueDto?>(leagueSlug, DtoFactory.ToDtoExpression());

        if (league == null)
            return NotFound(leagueSlug);

        return Ok(league);
    }

    [HttpGet("{leagueSlug}/seasons")]
    public async Task<ActionResult<List<SeasonDto>>> GetLeagueSeasons(string leagueSlug)
    {
        if (string.IsNullOrEmpty(leagueSlug))
            return BadRequest();

        var seasons = await this.seasonRepository.GetLeagueSeasonsAsync(leagueSlug, SeasonDtoFactory.ToDtoExpression());

        if (seasons == null)
            return NotFound(leagueSlug);

        return Ok(seasons);
    }

    [HttpGet("{leagueSlug}/seasons/{seasonSlug}")]
    public async Task<ActionResult<SeasonDto>> GetLeagueSeasons(string leagueSlug, string seasonSlug)
    {
        if (string.IsNullOrEmpty(leagueSlug) || string.IsNullOrEmpty(seasonSlug))
            return BadRequest();

        var season = await this.seasonRepository.GetBySlugAsync(leagueSlug, seasonSlug, SeasonDtoFactory.ToDtoExpression());

        if (season == null)
            return NotFound($"{leagueSlug}/{seasonSlug}");

        return Ok(season);
    }
}
