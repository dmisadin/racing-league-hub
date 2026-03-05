using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.BLL.Mapping.DtoFactories;
using RacingLeagueHub.BLL.Models.Dtos.Team;
using RacingLeagueHub.Entities;
using RacingLeagueHub.Infrastructure;

namespace RacingLeagueHub.Controllers.Admin
{
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
}
