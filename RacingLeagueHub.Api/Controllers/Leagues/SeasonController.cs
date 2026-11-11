using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Api.Authorization;
using RacingLeagueHub.Application.Seasons;
using RacingLeagueHub.Application.Seasons.Dtos;
using RacingLeagueHub.Application.Seasons.Persistence;
using RacingLeagueHub.Domain.Common.Models;
using RacingLeagueHub.Domain.Common.Models.Enums;

namespace RacingLeagueHub.Api.Controllers.Leagues;

[Route("api/leagues/{leagueSlug}/seasons")]
public class SeasonController : ApiController
{
    private const int PageSize = 10;

    private readonly ISeasonQueries seasonQueries;
    private readonly ISeasonCommands seasonCommands;
    private readonly ISeasonService seasonService;

    public SeasonController(
        ISeasonQueries seasonQueries,
        ISeasonCommands seasonCommands,
        ISeasonService seasonService)
    {
        this.seasonQueries = seasonQueries;
        this.seasonCommands = seasonCommands;
        this.seasonService = seasonService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<PagedResult<SeasonDto>>> GetPaged(
        [FromRoute] string leagueSlug,
        [FromQuery] int page = 1,
        CancellationToken ct = default)
    {
        var result = await seasonQueries.GetLeagueSeasonsAsync(
            leagueSlug,
            page,
            PageSize,
            ct);

        return Ok(result);
    }

    [HttpGet("overview")]
    [AllowAnonymous]
    public async Task<ActionResult<LeagueSeasonsOverviewDto>> GetOverview(
        [FromRoute] string leagueSlug,
        CancellationToken ct = default)
    {
        var overview = await seasonService.GetLeagueOverviewAsync(leagueSlug, PageSize, ct);

        if (overview is null)
            return NotFound();

        return Ok(overview);
    }

    [HttpGet("summaries")]
    [AllowAnonymous]
    public async Task<ActionResult<PagedResult<SeasonSummaryDto>>> GetSummariesPaged(
        [FromRoute] string leagueSlug,
        [FromQuery] SeasonStatus? status = null,
        [FromQuery] int page = 1,
        CancellationToken ct = default)
    {
        var result = await seasonService.GetLeagueSeasonSummariesAsync(
            leagueSlug,
            status,
            page,
            PageSize,
            ct);

        return Ok(result);
    }

    [HttpGet("{seasonSlug}")]
    [AllowAnonymous]
    public async Task<ActionResult<SeasonDto>> GetBySlug(
        [FromRoute] string leagueSlug,
        [FromRoute] string seasonSlug,
        CancellationToken ct = default)
    {
        var dto = await seasonQueries.GetBySlugAsync(
            leagueSlug,
            seasonSlug,
            ct);

        if (dto is null)
            return NotFound();

        return Ok(dto);
    }

    /// <summary>Season with assists, lobby settings and its divisions (with per-division calendar stats).</summary>
    [HttpGet("{seasonSlug}/details")]
    [AllowAnonymous]
    public async Task<ActionResult<SeasonDetailsDto>> GetDetails(
        [FromRoute] string leagueSlug,
        [FromRoute] string seasonSlug,
        CancellationToken ct = default)
    {
        var dto = await seasonService.GetDetailsAsync(leagueSlug, seasonSlug, ct);

        if (dto is null)
            return NotFound();

        return Ok(dto);
    }

    [HttpPost]
    [Authorize(Policy = LeaguePolicies.LeagueEditor)]
    public async Task<ActionResult<SeasonDto>> Create(
        [FromRoute] string leagueSlug,
        [FromBody] CreateSeasonDto createDto,
        CancellationToken ct = default)
    {
        var seasonDto = await seasonCommands.AddAsync(createDto, ct);

        return Ok(seasonDto);
    }

    [HttpPut("{seasonSlug}")]
    [Authorize(Policy = LeaguePolicies.LeagueEditor)]
    public async Task<ActionResult<int>> Update(
        [FromRoute] string leagueSlug,
        [FromRoute] string seasonSlug,
        [FromBody] UpdateSeasonDto dto,
        CancellationToken ct = default)
    {
        int? updatedId = await seasonCommands.UpdateBySlugAsync(leagueSlug, seasonSlug, dto, ct);

        if (updatedId is null)
            return NotFound();

        return Ok(updatedId.Value);
    }

    [HttpDelete("{seasonSlug}")]
    [Authorize(Policy = LeaguePolicies.LeagueOwner)]
    public async Task<IActionResult> Delete(
        [FromRoute] string leagueSlug,
        [FromRoute] string seasonSlug,
        CancellationToken ct = default)
    {
        var rows = await seasonCommands.DeleteBySlugAsync(leagueSlug, seasonSlug, ct);

        return rows == 0
            ? NotFound()
            : NoContent();
    }
}