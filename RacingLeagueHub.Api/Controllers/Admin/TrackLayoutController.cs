using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Application.DtoFactories;
using RacingLeagueHub.Application.Dtos.Track;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Infrastructure;

namespace RacingLeagueHub.Api.Controllers.Admin;

[Route("api/track-layout")]
[ApiController]
public class TrackLayoutController : GenericController<TrackLayout, TrackLayoutDto>
{
    public TrackLayoutController(IRepository<TrackLayout> repository) : base(repository)
    {
    }

    protected override IDtoFactory<TrackLayout, TrackLayoutDto> DtoFactory => new TrackLayoutDtoFactory();

    public override async Task<ActionResult<EncryptedId>> Update([FromRoute] EncryptedId id, [FromBody] TrackLayoutDto dto, CancellationToken ct = default)
    {
        var trackId = dto.Id?.RawId;
        if (trackId == null || trackId == 0)
            return BadRequest("Invalid ID.");

        var entityId = await this.repository.UpdateAsync(DtoFactory.FromDto, trackId.Value, dto);

        if (entityId == null)
            return NotFound();

        return Ok(new EncryptedId(entityId.Value));
    }
}
