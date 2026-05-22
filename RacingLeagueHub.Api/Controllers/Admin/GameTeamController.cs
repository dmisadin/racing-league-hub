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
[Route("api/game-team")]
[ApiController]
public class GameTeamController : GenericController<GameTeam, GameTeamDto>
{
    public GameTeamController(IRepository<GameTeam> genericRepository) : base(genericRepository)
    {
    }

    protected override IDtoMapper<GameTeam, GameTeamDto> DtoFactory => new GameTeamDtoFactory();

    public override Task<IActionResult> Delete([FromRoute] EncryptedId id, CancellationToken ct = default)
    {
        return Task.FromResult<IActionResult>(StatusCode(StatusCodes.Status403Forbidden));
    }
}
