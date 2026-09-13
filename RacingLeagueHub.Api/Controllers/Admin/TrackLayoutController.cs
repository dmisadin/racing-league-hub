using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Api.Authorization;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.TrackLayouts;
using RacingLeagueHub.Application.TrackLayouts.Dtos;

namespace RacingLeagueHub.Api.Controllers.Admin;

[Authorize(Policy = AppPolicies.SuperAdmin)]
[Route("api/track-layout")]
public class TrackLayoutController : ApiController
{
    private readonly ITrackLayoutService trackLayoutService;

    public TrackLayoutController(ITrackLayoutService trackLayoutService)
    {
        this.trackLayoutService = trackLayoutService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrackLayoutDto>> GetById(EncryptedId id, CancellationToken ct)
    {
        var track = await trackLayoutService.GetByIdAsync(id.RawId, ct);

        if (track is null)
            return NotFound();

        return Ok(track);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TrackLayoutDto>>> GetPaged([FromQuery] int page = 1, CancellationToken ct = default)
    {
        var tracks = await trackLayoutService.GetPagedAsync(page, ct);

        return Ok(tracks);
    }

    [HttpPost]
    public async Task<ActionResult<TrackLayoutDto>> AddTrack([FromBody] CreateTrackLayoutDto dto, CancellationToken ct)
    {
        var track = await trackLayoutService.AddAsync(dto, ct);

        return CreatedAtAction(nameof(GetById), new { id = track.Id }, track);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TrackLayoutDto>> UpdateTrack([FromRoute] EncryptedId id, [FromBody] UpdateTrackLayoutDto dto, CancellationToken ct)
    {
        var track = await trackLayoutService.UpdateAsync(id.RawId, dto, ct);

        if (track is null)
            return NotFound();

        return Ok(track);
    }
}
