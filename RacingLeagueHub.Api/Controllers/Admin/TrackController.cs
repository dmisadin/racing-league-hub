using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Api.Authorization;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Services.TrackService;
using RacingLeagueHub.Application.Services.TrackService.Dtos;
using RacingLeagueHub.Application.Services.TrackService.Dtos;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Infrastructure;
using RacingLeagueHub.Domain.Models.Constants;

namespace RacingLeagueHub.Api.Controllers.Admin;

[Authorize(Policy = AppPolicies.SuperAdmin)]
[Route("api/track")]
public class TrackController : ApiController
{
    private readonly ITrackService trackService;

    public TrackController(ITrackService trackService)
    {
        this.trackService = trackService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrackDto>> GetById(EncryptedId id, CancellationToken ct)
    {
        var track = await trackService.GetByIdAsync(id.RawId, ct);

        if (track is null)
            return NotFound();

        return Ok(track);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TrackDto>>> GetPaged([FromQuery] int page = 1, CancellationToken ct = default)
    {
        var tracks = await trackService.GetPagedAsync(page, ct);

        return Ok(tracks);
    }

    [HttpPost]
    public async Task<ActionResult<TrackDto>> AddTrack([FromBody] CreateTrackDto dto, CancellationToken ct)
    {
        var track = await trackService.AddAsync(dto, ct);

        return CreatedAtAction(nameof(GetById), new { id = track.Id }, track);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TrackDto>> UpdateTrack([FromRoute] EncryptedId id, [FromBody] UpdateTrackDto dto, CancellationToken ct)
    {
        var track = await trackService.UpdateAsync(id.RawId, dto, ct);

        if (track is null)
            return NotFound();

        return Ok(track);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrack(EncryptedId id, CancellationToken ct)
    {
        var deleted = await trackService.DeleteAsync(id.RawId, ct);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
    /*
    [HttpGet("get-all")]
    public virtual async Task<ActionResult<List<TrackDto>>> GetAll()
    {
        var dtos = await repository.GetAllAsync(dtoMapper.ToDtoExpression());

        if (dtos == null)
            return NotFound();

        foreach (var dto in dtos)
        {
            if (Countries.ByAlpha2.TryGetValue(dto.CountryAlpha2, out var country))
                dto.Country = country;
        }

        return Ok(dtos);
    }

    [HttpGet("lookup")]
    public async Task<ActionResult<LookupDto>> GetLookup()
    {
        var tracks = await repository.GetAllAsync(dtoMapper.ToDtoExpression());

        var lookups = tracks.Select(x => new LookupDto
        {
            Id = x.Id,
            Label = x.Name
        });

        return Ok(lookups);
    }
    */
}
