using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.API.DtoFactories;
using RacingLeagueHub.API.DtoFactories.Admin;
using RacingLeagueHub.API.Dtos.Track;
using RacingLeagueHub.BLL.Entities;
using RacingLeagueHub.BLL.Infrastructure;
using RacingLeagueHub.BLL.Models;

namespace RacingLeagueHub.API.Controllers.Admin;

[Route("api/track-layout")]
[ApiController]
public class TrackLayoutController : GenericController<TrackLayout, TrackLayoutDto>
{
    public TrackLayoutController(IRepository<TrackLayout> genericRepository) : base(genericRepository)
    {
    }

    protected override IDtoFactory<TrackLayout, TrackLayoutDto> DtoFactory => new TrackLayoutDtoFactory();

    public async override Task<ActionResult<EncryptedId>> Update([FromBody] TrackLayoutDto dto)
    {
        var id = dto.Id.RawId;
        if (id == 0)
            return BadRequest("Invalid ID.");

        var entity = await this.repository.Query()
                                    .Where(e => e.Id == id)
                                    .Include(e => e.TrackLayoutGames)
                                    .FirstOrDefaultAsync();

        if (entity == null)
            return NotFound();

        DtoFactory.FromDto(entity, dto);

        await this.repository.CommitAsync();

        return Ok(new EncryptedId(entity.Id));
    }
}
