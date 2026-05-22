using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Api.Authorization;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Abstractions;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Entities.Seasons;

namespace RacingLeagueHub.Api.Controllers.Leagues;


[Route("api/leagues")]
public class LeagueController : BaseController
{
    private const int PageSize = 10;

    private readonly ILeagueRepository leagueRepository;

    private readonly IDtoMapper<League, LeagueDto> dtoFactory = new LeagueDtoFactory();

    public LeagueController(ILeagueRepository leagueRepository)
    {
        this.leagueRepository = leagueRepository;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<PagedResult<LeagueDto>>> GetPaged(
        [FromQuery] int page = 1,
        CancellationToken ct = default)
    {
        var result = await leagueRepository.GetPagedAsync(
            dtoFactory.ToDtoExpression(),
            page,
            PageSize,
            ct);

        return Ok(result);
    }

    [HttpGet("{leagueSlug}")]
    [AllowAnonymous]
    public async Task<ActionResult<LeagueDto>> GetBySlug(
        [FromRoute] string leagueSlug,
        CancellationToken ct = default)
    {
        var dto = await leagueRepository.GetBySlugAsync(
            leagueSlug,
            dtoFactory.ToDtoExpression(),
            ct);

        if (dto is null)
            return NotFound();

        return Ok(dto);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<EncryptedId>> Create(
        [FromBody] LeagueDto dto,
        CancellationToken ct = default)
    {
        var entity = leagueRepository.Create();

        dtoFactory.FromDto(entity, dto);

        await leagueRepository.InsertAsync(entity);
        await leagueRepository.CommitAsync(ct);

        return Ok(new EncryptedId(entity.Id));
    }

    [HttpPut("{leagueSlug}")]
    [Authorize(Policy = LeaguePolicies.LeagueEditor)]
    public async Task<ActionResult<EncryptedId>> Update(
        [FromRoute] string leagueSlug,
        [FromBody] LeagueDto dto,
        CancellationToken ct = default)
    {
        var league = await leagueRepository.GetBySlugAsync(
            leagueSlug,
            x => new
            {
                x.Id
            },
            ct);

        if (league is null)
            return NotFound();

        if (dto.Id is not null && dto.Id.RawId != league.Id)
            return BadRequest("Route league slug does not match body ID.");

        var updatedId = await leagueRepository.UpdateAsync(
            dtoFactory.FromDto,
            league.Id,
            dto);

        if (updatedId is null)
            return NotFound();

        await leagueRepository.CommitAsync(ct);

        return Ok(new EncryptedId(updatedId.Value));
    }

    [HttpDelete("{leagueSlug}")]
    [Authorize(Policy = LeaguePolicies.LeagueOwner)]
    public async Task<IActionResult> Delete(
        [FromRoute] string leagueSlug,
        CancellationToken ct = default)
    {
        var rows = await leagueRepository.ExecuteDeleteAsync(
            x => x.Slug == leagueSlug,
            ct);

        return rows == 0
            ? NotFound()
            : NoContent();
    }
}
