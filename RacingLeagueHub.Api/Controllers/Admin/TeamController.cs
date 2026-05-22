using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Api.Authorization;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Dtos.Team;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Infrastructure;

namespace RacingLeagueHub.Api.Controllers.Admin;

[Authorize(Policy = AppPolicies.SuperAdmin)]
[Route("api/team")]
[ApiController]
public class TeamController : GenericController<Team, TeamDto>
{
    public TeamController(IRepository<Team> genericRepository) : base(genericRepository)
    {
    }

    protected override IDtoMapper<Team, TeamDto> DtoMapper => new TeamDtoMapper();

    public override Task<IActionResult> Delete([FromRoute] EncryptedId id, CancellationToken ct = default)
    {
        return Task.FromResult<IActionResult>(StatusCode(StatusCodes.Status403Forbidden));
    }

    [HttpGet("get-all")]
    public virtual async Task<ActionResult<List<TeamDto>>> GetAll()
    {
        var dtos = await repository.GetAllAsync(DtoMapper.ToDtoExpression());

        if (dtos == null)
            return NotFound();

        return Ok(dtos);
    }
}
