using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.BLL.Mapping.DtoFactories;
using RacingLeagueHub.BLL.Models.Dtos.Team;
using RacingLeagueHub.Entities;
using RacingLeagueHub.Infrastructure;

namespace RacingLeagueHub.Controllers.Admin;

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
}
