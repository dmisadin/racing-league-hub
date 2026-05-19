using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Application.DtoFactories;
using RacingLeagueHub.Application.Dtos.Team;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Infrastructure;

namespace RacingLeagueHub.Api.Controllers.Admin;


[Route("api/game-team")]
[ApiController]
public class GameTeamController : GenericController<GameTeam, GameTeamDto>
{
    public GameTeamController(IRepository<GameTeam> genericRepository) : base(genericRepository)
    {
    }

    protected override IDtoFactory<GameTeam, GameTeamDto> DtoFactory => new GameTeamDtoFactory();

    public override Task<IActionResult> Delete([FromRoute] EncryptedId id, CancellationToken ct = default)
    {
        return Task.FromResult<IActionResult>(StatusCode(StatusCodes.Status403Forbidden));
    }
}
