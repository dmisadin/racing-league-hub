using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.BLL.Entities;
using RacingLeagueHub.BLL.Mapping.DtoFactories;
using RacingLeagueHub.BLL.Mapping.DtoFactories.Admin;
using RacingLeagueHub.BLL.Models.Dtos.Track;
using RacingLeagueHub.Infrastructure;

namespace RacingLeagueHub.API.Controllers.Admin;

[Route("api/track-layout")]
[ApiController]
public class TrackLayoutController : GenericController<TrackLayout, TrackLayoutDto>
{
    public TrackLayoutController(IRepository<TrackLayout> genericRepository) : base(genericRepository)
    {
    }

    protected override IDtoFactory<TrackLayout, TrackLayoutDto> DtoFactory => new TrackLayoutDtoFactory();

    public async override Task<ActionResult<long>> Update([FromBody] TrackLayoutDto dto)
    {
        var id = dto.Id;
        if (id == null || id == 0)
            return BadRequest("Invalid ID.");

        var entity = await this.repository.Query()
                                    .Where(e => e.Id == id)
                                    .Include(e => e.TrackLayoutGames)
                                    .FirstOrDefaultAsync();

        if (entity == null)
            return NotFound();

        DtoFactory.FromDto(entity, dto);

        await this.repository.CommitAsync();

        return Ok(entity.Id);
    }
}
