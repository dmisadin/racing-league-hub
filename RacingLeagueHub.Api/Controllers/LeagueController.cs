using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Api.Controllers.Leagues;
using RacingLeagueHub.Application.DtoFactories;
using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Application.Dtos.GrandPrix;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Abstractions;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Entities.GrandsPrix;
using RacingLeagueHub.Domain.Entities.Seasons;

namespace RacingLeagueHub.Api.Controllers;

[Route("api/leagues")]
public class LeagueController : GenericLeagueController<League, LeagueDto>
{
    private readonly ILeagueRepository leagueRepository;
    private readonly ISeasonRepository seasonRepository;
    private readonly IGrandPrixRepository grandPrixRepository;

    public LeagueController(ILeagueRepository repository, 
                            ISeasonRepository seasonRepository,
                            IGrandPrixRepository grandPrixRepository) 
        : base(repository)
    {
        this.leagueRepository = repository;
        this.seasonRepository = seasonRepository;
        this.grandPrixRepository = grandPrixRepository;
    }

    protected override IDtoFactory<League, LeagueDto> DtoFactory => new LeagueDtoFactory();
    protected IDtoFactory<Season, SeasonDto> SeasonDtoFactory => new SeasonDtoFactory();
    protected IDtoFactory<GrandPrix, GrandPrixDto> GrandPrixDtoFactory => new GrandPrixDtoFactory();

    [AllowAnonymous]
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

    [AllowAnonymous]
    [HttpGet("{leagueSlug}/seasons")]
    public async Task<ActionResult<PagedResult<SeasonDto>>> GetAllPaginated(string leagueSlug, [FromQuery] int page = 1, CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(leagueSlug))
            return BadRequest();

        var seasons = await seasonRepository.GetLeagueSeasonsAsync(leagueSlug, SeasonDtoFactory.ToDtoExpression(), page, ct: ct);

        if (seasons == null)
            return NotFound(leagueSlug);

        return Ok(seasons);
    }

    [AllowAnonymous]
    [HttpGet("{leagueSlug}/seasons/{seasonSlug}")]
    public async Task<ActionResult<SeasonDto>> GetSeasonBySlug(string leagueSlug, string seasonSlug, CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(leagueSlug) || string.IsNullOrEmpty(seasonSlug))
            return BadRequest();

        var season = await this.seasonRepository.GetBySlugAsync(leagueSlug, seasonSlug, SeasonDtoFactory.ToDtoExpression(), ct);

        if (season == null)
            return NotFound($"{leagueSlug}/{seasonSlug}");

        return Ok(season);
    }

    [AllowAnonymous]
    [HttpGet("{leagueSlug}/seasons/{seasonSlug}/grands-prix")]
    public async Task<ActionResult<PagedResult<SeasonDto>>> GetAllGrandsPrixPaginated(string leagueSlug, string seasonSlug, [FromQuery] int page = 1, CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(leagueSlug) || string.IsNullOrEmpty(seasonSlug))
            return BadRequest();

        var grandsPrix = await grandPrixRepository.GetSeasonGrandsPrixAsync(leagueSlug, seasonSlug, GrandPrixDtoFactory.ToDtoExpression(), page, ct: ct);

        if (grandsPrix == null)
            return NotFound($"{leagueSlug}/{seasonSlug}");

        return Ok(grandsPrix);
    }
    
    [AllowAnonymous]
    [HttpGet("{leagueSlug}/seasons/{seasonSlug}/grands-prix/{grandPrixSlug}")]
    public async Task<ActionResult<GrandPrixDto>> GetGrandPrixBySlug(string leagueSlug, string seasonSlug, string grandPrixSlug, CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(leagueSlug) || string.IsNullOrEmpty(seasonSlug) || string.IsNullOrEmpty(grandPrixSlug))
            return BadRequest();

        var grandPrix = await this.grandPrixRepository.GetBySlugAsync(leagueSlug, seasonSlug, grandPrixSlug, GrandPrixDtoFactory.ToDtoExpression(), ct);

        if (grandPrix == null)
            return NotFound($"{leagueSlug}/{seasonSlug}/{grandPrixSlug}");

        return Ok(grandPrix);
    }
}
