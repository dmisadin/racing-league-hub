using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Api.Authorization;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.GrandsPrix.Dtos;
using RacingLeagueHub.Application.GrandsPrix.Persistence;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Seasons.Persistence;
using RacingLeagueHub.Domain.Abstractions;
using RacingLeagueHub.Domain.Entities.GrandsPrix;

namespace RacingLeagueHub.Api.Controllers.Leagues;

[Route("api/leagues/{leagueSlug}/seasons/{seasonSlug}/grands-prix")]
public class GrandPrixController : ApiController
{
    private const int PageSize = 10;

    private readonly IGrandPrixCommands grandPrixCommands;
    private readonly IGrandPrixQueries grandPrixQueries;
    private readonly ISeasonQueries seasonQueries;
    private readonly IDtoMapper<GrandPrix, GrandPrixDto> dtoMapper;

    public GrandPrixController(
        IGrandPrixCommands grandPrixCommands,
        IGrandPrixQueries grandPrixQueries,
        ISeasonQueries seasonQueries,
        IDtoMapper<GrandPrix, GrandPrixDto> dtoMapper)
    {
        this.grandPrixCommands = grandPrixCommands;
        this.grandPrixQueries = grandPrixQueries;
        this.seasonQueries = seasonQueries;
        this.dtoMapper = dtoMapper;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<PagedResult<GrandPrixDto>>> GetPaged(
        [FromRoute] string leagueSlug,
        [FromRoute] string seasonSlug,
        [FromQuery] int page = 1,
        CancellationToken ct = default)
    {
        var result = await grandPrixQueries.GetSeasonGrandsPrixAsync(
            leagueSlug,
            seasonSlug,
            page,
            PageSize,
            ct);

        if (result is null)
            return NotFound("Season not found.");

        return Ok(result);
    }

    [HttpGet("{grandPrixSlug}")]
    [AllowAnonymous]
    public async Task<ActionResult<GrandPrixDto>> GetBySlug(
        [FromRoute] string leagueSlug,
        [FromRoute] string seasonSlug,
        [FromRoute] string grandPrixSlug,
        CancellationToken ct = default)
    {
        var dto = await grandPrixQueries.GetBySlugAsync(
            leagueSlug,
            seasonSlug,
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
        [FromBody] CreateGrandPrixDto dto,
        CancellationToken ct = default)
    {
        var season = await seasonQueries.GetBySlugAsync(leagueSlug, seasonSlug, ct);

        if (season is null)
            return NotFound("Season not found.");

        dto.SeasonId = season.Id;

        var grandPrixDto = await grandPrixCommands.AddAsync(dto, ct);

        return Ok(grandPrixDto);
    }

    [HttpPut("{grandPrixSlug}")]
    [Authorize(Policy = LeaguePolicies.LeagueEditor)]
    public async Task<ActionResult<EncryptedId>> Update(
        [FromRoute] string leagueSlug,
        [FromRoute] string seasonSlug,
        [FromRoute] string grandPrixSlug,
        [FromBody] UpdateGrandPrixDto dto,
        CancellationToken ct = default)
    {
        var updatedId = await grandPrixCommands.UpdateBySlugAsync(
            leagueSlug,
            seasonSlug,
            grandPrixSlug,
            dto,
            ct);

        if (updatedId is null)
            return NotFound();

        return Ok(new EncryptedId(updatedId.Value));
    }


    [HttpDelete("{grandPrixSlug}")]
    [Authorize(Policy = LeaguePolicies.LeagueOwner)]
    public async Task<IActionResult> Delete(
        [FromRoute] string leagueSlug,
        [FromRoute] string seasonSlug,
        [FromRoute] string grandPrixSlug,
        CancellationToken ct = default)
    {
        var rows = await grandPrixCommands.DeleteBySlugAsync(grandPrixSlug, seasonSlug, leagueSlug, ct);

        return rows == 0
            ? NotFound()
            : NoContent();
    }
}