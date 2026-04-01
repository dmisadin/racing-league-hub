using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Application.DtoFactories;
using RacingLeagueHub.Application.Dtos.Team;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.BLL.Entities;
using RacingLeagueHub.BLL.Infrastructure;

namespace RacingLeagueHub.API.Controllers.Admin;


[Route("api/game-team")]
[ApiController]
public class GameTeamController : GenericController<GameTeam, GameTeamDto>
{
    public GameTeamController(IRepository<GameTeam> genericRepository) : base(genericRepository)
    {
    }

    protected override IDtoFactory<GameTeam, GameTeamDto> DtoFactory => new GameTeamDtoFactory();

    public override Task<IActionResult> Delete([FromRoute] EncryptedId id)
    {
        return Task.FromResult<IActionResult>(StatusCode(StatusCodes.Status403Forbidden));
    }
}
