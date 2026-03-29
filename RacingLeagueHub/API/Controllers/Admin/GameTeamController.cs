using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.API.DtoFactories;
using RacingLeagueHub.API.Dtos.Team;
using RacingLeagueHub.BLL.Entities;
using RacingLeagueHub.BLL.Infrastructure;
using RacingLeagueHub.BLL.Models;

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
