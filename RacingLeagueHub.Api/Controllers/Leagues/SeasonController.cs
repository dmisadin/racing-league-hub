using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Api.Authorization;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Seasons;
using RacingLeagueHub.Application.Seasons.Dtos;
using RacingLeagueHub.Application.Seasons.Persistence;

namespace RacingLeagueHub.Api.Controllers.Leagues;

[Route("api/leagues/{leagueSlug}/seasons")]
public class SeasonController : ApiController
{
    private const int PageSize = 10;

    private readonly ISeasonQueries seasonQueries;
    private readonly ISeasonCommands seasonCommands;

    public SeasonController(
        ISeasonQueries seasonQueries,
        ISeasonCommands seasonCommands,
        ISeasonService seasonService)
    {
        this.seasonQueries = seasonQueries;
        this.seasonCommands = seasonCommands;
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
    public async Task<ActionResult<long>> Update(
        [FromRoute] string leagueSlug,
        [FromRoute] string seasonSlug,
        [FromBody] UpdateSeasonDto dto,
        CancellationToken ct = default)
    {
        long? updatedId = await seasonCommands.UpdateBySlugAsync(leagueSlug, seasonSlug, dto, ct);

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
        var rows = await seasonCommands.DeleteBySlugAsync(seasonSlug, leagueSlug, ct);

        return rows == 0
            ? NotFound()
            : NoContent();
    }
}