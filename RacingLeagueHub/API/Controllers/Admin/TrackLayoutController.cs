using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Application.DtoFactories;
using RacingLeagueHub.Application.Dtos.Track;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.BLL.Entities;
using RacingLeagueHub.BLL.Infrastructure;

namespace RacingLeagueHub.API.Controllers.Admin;

[Route("api/track-layout")]
[ApiController]
public class TrackLayoutController : GenericController<TrackLayout, TrackLayoutDto>
{
    public TrackLayoutController(IRepository<TrackLayout> repository) : base(repository)
    {
    }

    protected override IDtoFactory<TrackLayout, TrackLayoutDto> DtoFactory => new TrackLayoutDtoFactory();

    public async override Task<ActionResult<EncryptedId>> Update([FromBody] TrackLayoutDto dto)
    {
        var id = dto.Id?.RawId;
        if (id == null || id == 0)
            return BadRequest("Invalid ID.");

        var entityId = await this.repository.UpdateAsync<TrackLayoutDto>(DtoFactory.FromDto, id.Value, dto);

        if (entityId == null)
            return NotFound();

        return Ok(new EncryptedId(entityId.Value));
    }
}
