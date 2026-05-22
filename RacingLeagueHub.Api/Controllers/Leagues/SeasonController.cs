using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Api.Authorization;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Abstractions;
using RacingLeagueHub.Domain.Entities.Seasons;

namespace RacingLeagueHub.Api.Controllers.Leagues;

[ApiController]
[Route("api/leagues/{leagueSlug}/seasons")]
public class SeasonController : BaseController
{
    private const int PageSize = 10;

    private readonly ISeasonRepository seasonRepository;
    private readonly ILeagueRepository leagueRepository;

    private readonly IDtoMapper<Season, SeasonDto> dtoFactory =
        new SeasonDtoMapper();

    public SeasonController(
        ISeasonRepository seasonRepository,
        ILeagueRepository leagueRepository)
    {
        this.seasonRepository = seasonRepository;
        this.leagueRepository = leagueRepository;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<PagedResult<SeasonDto>>> GetPaged(
        [FromRoute] string leagueSlug,
        [FromQuery] int page = 1,
        CancellationToken ct = default)
    {
        var result = await seasonRepository.GetLeagueSeasonsAsync(
            leagueSlug,
            dtoFactory.ToDtoExpression(),
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
        var dto = await seasonRepository.GetBySlugAsync(
            leagueSlug,
            seasonSlug,
            dtoFactory.ToDtoExpression(),
            ct);

        if (dto is null)
            return NotFound();

        return Ok(dto);
    }

    [HttpPost]
    [Authorize(Policy = LeaguePolicies.LeagueEditor)]
    public async Task<ActionResult<EncryptedId>> Create(
        [FromRoute] string leagueSlug,
        [FromBody] SeasonDto dto,
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
            return NotFound("League not found.");

        var entity = seasonRepository.Create();

        dtoFactory.FromDto(entity, dto);

        entity.LeagueId = league.Id;

        await seasonRepository.InsertAsync(entity);
        await seasonRepository.CommitAsync(ct);

        return Ok(new EncryptedId(entity.Id));
    }

    [HttpPut("{seasonSlug}")]
    [Authorize(Policy = LeaguePolicies.LeagueEditor)]
    public async Task<ActionResult<EncryptedId>> Update(
        [FromRoute] string leagueSlug,
        [FromRoute] string seasonSlug,
        [FromBody] SeasonDto dto,
        CancellationToken ct = default)
    {
        var season = await seasonRepository.GetBySlugAsync(
            leagueSlug,
            seasonSlug,
            x => new
            {
                x.Id,
                x.LeagueId
            },
            ct);

        if (season is null)
            return NotFound();

        if (dto.Id is not null && dto.Id.RawId != season.Id)
            return BadRequest("Route season slug does not match body ID.");

        var updatedId = await seasonRepository.UpdateAsync(
            (entity, seasonDto) =>
            {
                var originalLeagueId = entity.LeagueId;
                var changed = dtoFactory.FromDto(entity, seasonDto);
                entity.LeagueId = originalLeagueId;
                return changed;
            },
            season.Id,
            dto);

        if (updatedId is null)
            return NotFound();

        return Ok(new EncryptedId(updatedId.Value));
    }

    [HttpDelete("{seasonSlug}")]
    [Authorize(Policy = LeaguePolicies.LeagueOwner)]
    public async Task<IActionResult> Delete(
        [FromRoute] string leagueSlug,
        [FromRoute] string seasonSlug,
        CancellationToken ct = default)
    {
        var rows = await seasonRepository.ExecuteDeleteAsync(
            x => x.Slug == seasonSlug &&
                 x.League.Slug == leagueSlug,
            ct);

        return rows == 0
            ? NotFound()
            : NoContent();
    }
}