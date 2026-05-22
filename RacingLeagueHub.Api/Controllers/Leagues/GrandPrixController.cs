using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Api.Authorization;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Dtos.GrandPrix;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Abstractions;
using RacingLeagueHub.Domain.Entities.GrandsPrix;

namespace RacingLeagueHub.Api.Controllers.Leagues;

[Route("api/leagues/{leagueSlug}/seasons/{seasonSlug}/grands-prix")]
public class GrandPrixController : BaseController
{
    private const int PageSize = 10;

    private readonly IGrandPrixRepository grandPrixRepository;
    private readonly ISeasonRepository seasonRepository;

    private readonly IDtoMapper<GrandPrix, GrandPrixDto> DtoMapper =
        new GrandPrixDtoMapper();

    public GrandPrixController(
        IGrandPrixRepository grandPrixRepository,
        ISeasonRepository seasonRepository)
    {
        this.grandPrixRepository = grandPrixRepository;
        this.seasonRepository = seasonRepository;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<PagedResult<GrandPrixDto>>> GetPaged(
        [FromRoute] string leagueSlug,
        [FromRoute] string seasonSlug,
        [FromQuery] int page = 1,
        CancellationToken ct = default)
    {
        var result = await grandPrixRepository.GetSeasonGrandsPrixAsync(
            leagueSlug,
            seasonSlug,
            DtoMapper.ToDtoExpression(),
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
        var dto = await grandPrixRepository.GetBySlugAsync(
            leagueSlug,
            seasonSlug,
            grandPrixSlug,
            DtoMapper.ToDtoExpression(),
            ct);

        if (dto is null)
            return NotFound();

        return Ok(dto);
    }

    [HttpPost]
    [Authorize(Policy = LeaguePolicies.LeagueEditor)]
    public async Task<ActionResult<EncryptedId>> Create(
        [FromRoute] string leagueSlug,
        [FromRoute] string seasonSlug,
        [FromBody] GrandPrixDto dto,
        CancellationToken ct = default)
    {
        var season = await seasonRepository.GetBySlugAsync(
            leagueSlug,
            seasonSlug,
            season => new
            {
                season.Id
            },
            ct);

        if (season is null)
            return NotFound("Season not found.");

        var entity = grandPrixRepository.Create();

        DtoMapper.FromDto(entity, dto);

        entity.SeasonId = season.Id;

        await grandPrixRepository.InsertAsync(entity);
        await grandPrixRepository.CommitAsync(ct);

        return Ok(new EncryptedId(entity.Id));
    }

    [HttpPut("{grandPrixSlug}")]
    [Authorize(Policy = LeaguePolicies.LeagueEditor)]
    public async Task<ActionResult<EncryptedId>> Update(
        [FromRoute] string leagueSlug,
        [FromRoute] string seasonSlug,
        [FromRoute] string grandPrixSlug,
        [FromBody] GrandPrixDto dto,
        CancellationToken ct = default)
    {
        var updatedId = await grandPrixRepository.UpdateBySlugAsync(
            leagueSlug,
            seasonSlug,
            grandPrixSlug,
            DtoMapper.FromDto,
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
        var rows = await grandPrixRepository.ExecuteDeleteAsync(x =>
            x.Slug == grandPrixSlug &&
            x.Season.Slug == seasonSlug &&
            x.Season.League.Slug == leagueSlug);

        return rows == 0
            ? NotFound()
            : NoContent();
    }
}