using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Api.Authorization;
using RacingLeagueHub.Application.GrandsPrix;
using RacingLeagueHub.Application.GrandsPrix.Dtos;
using RacingLeagueHub.Application.GrandsPrix.Persistence;
using RacingLeagueHub.Domain.Common.Models;

namespace RacingLeagueHub.Api.Controllers.Leagues;

/// <remarks>
/// Grand Prix slugs are unique per season, so the flat route resolves any Grand Prix of the season.
/// The <c>division/{divisionSlug}</c> route narrows every action to that division's own calendar.
/// </remarks>
[Route("api/leagues/{leagueSlug}/seasons/{seasonSlug}/grands-prix")]
[Route("api/leagues/{leagueSlug}/seasons/{seasonSlug}/division/{divisionSlug}/grands-prix")]
public class GrandPrixController : ApiController
{
    private const int PageSize = 10;

    private readonly IGrandPrixCommands grandPrixCommands;
    private readonly IGrandPrixQueries grandPrixQueries;
    private readonly IGrandPrixService grandPrixService;

    public GrandPrixController(
        IGrandPrixCommands grandPrixCommands,
        IGrandPrixQueries grandPrixQueries,
        IGrandPrixService grandPrixService)
    {
        this.grandPrixCommands = grandPrixCommands;
        this.grandPrixQueries = grandPrixQueries;
        this.grandPrixService = grandPrixService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<PagedResult<GrandPrixDto>>> GetPaged(
        [FromRoute] string leagueSlug,
        [FromRoute] string seasonSlug,
        [FromRoute] string? divisionSlug,
        [FromQuery] int page = 1,
        CancellationToken ct = default)
    {
        var result = await grandPrixQueries.GetSeasonGrandsPrixAsync(
            leagueSlug,
            seasonSlug,
            divisionSlug,
            page,
            PageSize,
            ct);

        return Ok(result);
    }

    [HttpGet("{grandPrixSlug}")]
    [AllowAnonymous]
    public async Task<ActionResult<GrandPrixDto>> GetBySlug(
        [FromRoute] string leagueSlug,
        [FromRoute] string seasonSlug,
        [FromRoute] string? divisionSlug,
        [FromRoute] string grandPrixSlug,
        CancellationToken ct = default)
    {
        var dto = await grandPrixQueries.GetBySlugAsync(
            leagueSlug,
            seasonSlug,
            divisionSlug,
            grandPrixSlug,
            ct);

        if (dto is null)
            return NotFound();

        return Ok(dto);
    }

    [HttpPost]
    [Authorize(Policy = LeaguePolicies.LeagueEditor)]
    public async Task<ActionResult<GrandPrixDto>> Create(
        [FromRoute] string leagueSlug,
        [FromRoute] string seasonSlug,
        [FromRoute] string? divisionSlug,
        [FromBody] CreateGrandPrixDto dto,
        CancellationToken ct = default)
    {
        var grandPrixDto = await grandPrixService.CreateAsync(
            leagueSlug,
            seasonSlug,
            divisionSlug,
            dto,
            ct);

        if (grandPrixDto is null)
            return NotFound("Season or division not found.");

        return Ok(grandPrixDto);
    }

    [HttpPut("{grandPrixSlug}")]
    [Authorize(Policy = LeaguePolicies.LeagueEditor)]
    public async Task<ActionResult<int>> Update(
        [FromRoute] string leagueSlug,
        [FromRoute] string seasonSlug,
        [FromRoute] string? divisionSlug,
        [FromRoute] string grandPrixSlug,
        [FromBody] UpdateGrandPrixDto dto,
        CancellationToken ct = default)
    {
        var updatedId = await grandPrixCommands.UpdateBySlugAsync(
            leagueSlug,
            seasonSlug,
            divisionSlug,
            grandPrixSlug,
            dto,
            ct);

        if (updatedId is null)
            return NotFound();

        return Ok(updatedId.Value);
    }

    [HttpDelete("{grandPrixSlug}")]
    [Authorize(Policy = LeaguePolicies.LeagueOwner)]
    public async Task<IActionResult> Delete(
        [FromRoute] string leagueSlug,
        [FromRoute] string seasonSlug,
        [FromRoute] string? divisionSlug,
        [FromRoute] string grandPrixSlug,
        CancellationToken ct = default)
    {
        var rows = await grandPrixCommands.DeleteBySlugAsync(
            leagueSlug,
            seasonSlug,
            divisionSlug,
            grandPrixSlug,
            ct);

        return rows == 0
            ? NotFound()
            : NoContent();
    }
}
