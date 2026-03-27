using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.API.DtoFactories;
using RacingLeagueHub.API.Dtos.Team;
using RacingLeagueHub.BLL.Entities;
using RacingLeagueHub.Infrastructure;

namespace RacingLeagueHub.API.Controllers.Admin;

[Route("api/team")]
[ApiController]
public class TeamController : GenericController<Team, TeamDto>
{
    public TeamController(IRepository<Team> genericRepository) : base(genericRepository)
    {
    }

    protected override IDtoFactory<Team, TeamDto> DtoFactory => new TeamDtoFactory();

    public override Task<IActionResult> Delete(long id)
    {
        return Task.FromResult<IActionResult>(StatusCode(StatusCodes.Status403Forbidden));
    }

    [HttpGet("get-all")]
    public virtual async Task<ActionResult<List<TeamDto>>> GetAll()
    {
        var dtos = await repository.Query()
            .Select(DtoFactory.ToDtoExpression())
            .ToListAsync();

        if (dtos == null)
            return NotFound();

        return Ok(dtos);
    }
}
