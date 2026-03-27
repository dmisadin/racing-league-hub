using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.BLL.Entities;
using RacingLeagueHub.API.Dtos.Team;
using RacingLeagueHub.API.DtoFactories;
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

    public override Task<IActionResult> Delete(long id)
    {
        return Task.FromResult<IActionResult>(StatusCode(StatusCodes.Status403Forbidden));
    }
}
