using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Application.DtoFactories;
using RacingLeagueHub.Application.Dtos.Team;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Infrastructure;

namespace RacingLeagueHub.Api.Controllers.Admin;

[Route("api/team")]
[ApiController]
public class TeamController : GenericController<Team, TeamDto>
{
    public TeamController(IRepository<Team> genericRepository) : base(genericRepository)
    {
    }

    protected override IDtoFactory<Team, TeamDto> DtoFactory => new TeamDtoFactory();

    public override Task<IActionResult> Delete([FromRoute] EncryptedId id)
    {
        return Task.FromResult<IActionResult>(StatusCode(StatusCodes.Status403Forbidden));
    }

    [HttpGet("get-all")]
    public virtual async Task<ActionResult<List<TeamDto>>> GetAll()
    {
        var dtos = await repository.GetAllAsync(DtoFactory.ToDtoExpression());

        if (dtos == null)
            return NotFound();

        return Ok(dtos);
    }
}
