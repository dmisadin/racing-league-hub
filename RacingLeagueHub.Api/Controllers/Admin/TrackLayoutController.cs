using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Api.Authorization;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Dtos.Track;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Abstractions.Admin;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Api.Controllers.Admin;

[Authorize(Policy = AppPolicies.SuperAdmin)]
[Route("api/track-layout")]
[ApiController]
public class TrackLayoutController : GenericController<TrackLayout, TrackLayoutDto>
{
    private readonly ITrackLayoutRepository trackLayoutRepository;

    public TrackLayoutController(ITrackLayoutRepository repository) : base(repository)
    {
        this.trackLayoutRepository = repository;
    }

    protected override IDtoMapper<TrackLayout, TrackLayoutDto> DtoFactory => new TrackLayoutDtoMapper();

    public override async Task<ActionResult<EncryptedId>> Update([FromRoute] EncryptedId id, [FromBody] TrackLayoutDto dto, CancellationToken ct = default)
    {
        var trackId = dto.Id?.RawId;
        if (trackId == null || trackId == 0)
            return BadRequest("Invalid ID.");

        var entityId = await trackLayoutRepository.UpdateAsync(DtoFactory.FromDto, trackId.Value, dto, ct);

        if (entityId == null)
            return NotFound();

        return Ok(new EncryptedId(entityId.Value));
    }
}
