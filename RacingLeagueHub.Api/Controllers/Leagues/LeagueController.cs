using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Api.Authorization;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Leagues.Dtos;
using RacingLeagueHub.Application.Leagues.Persistence;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Api.Controllers.Leagues;


[Route("api/leagues")]
public class LeagueController : ApiController
{
    private const int PageSize = 10;

    private readonly ILeagueQueries leagueQueries;
    private readonly ILeagueCommands leagueCommands;
    private readonly IDtoMapper<League, LeagueDto> mapper;

    public LeagueController(ILeagueQueries leagueQueries,
        ILeagueCommands leagueCommands,
        IDtoMapper<League, LeagueDto> mapper)
    {
        this.leagueQueries = leagueQueries;
        this.leagueCommands = leagueCommands;
        this.mapper = mapper;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<PagedResult<LeagueDto>>> GetPaged(
        [FromQuery] int page = 1,
        CancellationToken ct = default)
    {
        var result = await leagueQueries.GetLeaguesAsync(page, PageSize, ct);

        return Ok(result);
    }

    [HttpGet("{leagueSlug}")]
    [AllowAnonymous]
    public async Task<ActionResult<LeagueDto>> GetBySlug(
        [FromRoute] string leagueSlug,
        CancellationToken ct = default)
    {
        var dto = await leagueQueries.GetBySlugAsync(leagueSlug, ct);

        if (dto is null)
            return NotFound();

        return Ok(dto);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<EncryptedId>> Create(
        [FromBody] CreateLeagueDto createDto,
        CancellationToken ct = default)
    {
        LeagueDto? dto = await leagueCommands.AddAsync(createDto, ct);

        return Ok(dto);
    }

    [HttpPut("{leagueSlug}")]
    [Authorize(Policy = LeaguePolicies.LeagueEditor)]
    public async Task<ActionResult<long>> Update(
        [FromRoute] string leagueSlug,
        [FromBody] UpdateLeagueDto updateDto,
        CancellationToken ct = default)
    {
        long? updatedId = await leagueCommands.UpdateBySlugAsync(leagueSlug, updateDto, ct);

        if (updatedId is null)
            return NotFound();

        return Ok(updatedId);
    }

    [HttpDelete("{leagueSlug}")]
    [Authorize(Policy = LeaguePolicies.LeagueOwner)]
    public async Task<IActionResult> Delete(
        [FromRoute] string leagueSlug,
        CancellationToken ct = default)
    {
        int rows = await leagueCommands.DeleteBySlugAsync(leagueSlug, ct);

        return rows == 0
            ? NotFound()
            : NoContent();
    }
}
