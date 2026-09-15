using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Api.Authorization;
using RacingLeagueHub.Application.GameTeams;
using RacingLeagueHub.Application.GameTeams.Dtos;
using RacingLeagueHub.Application.Teams.Dtos;

namespace RacingLeagueHub.Api.Controllers.Admin;

[Authorize(Policy = AppPolicies.SuperAdmin)]
[Route("api/game-team")]
public class GameTeamController : ApiController
{
    private readonly IGameTeamService gameTeamService;

    public GameTeamController(IGameTeamService gameTeamService)
    {
        this.gameTeamService = gameTeamService;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<TeamDto>> GetById(int id, CancellationToken ct)
    {
        var team = await gameTeamService.GetByIdAsync(id, ct);

        if (team is null)
            return NotFound();

        return Ok(team);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TeamDto>>> GetPaged([FromQuery] int page = 1, CancellationToken ct = default)
    {
        var teams = await gameTeamService.GetPagedAsync(page, ct);

        return Ok(teams);
    }

    [HttpPost]
    public async Task<ActionResult<TeamDto>> AddGameTeam([FromBody] CreateGameTeamDto dto, CancellationToken ct)
    {
        var team = await gameTeamService.AddAsync(dto, ct);

        return CreatedAtAction(nameof(GetById), new { id = team.Id }, team);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TeamDto>> UpdateGameTeam([FromRoute] int id, [FromBody] UpdateGameTeamDto dto, CancellationToken ct)
    {
        var team = await gameTeamService.UpdateAsync(id, dto, ct);

        if (team is null)
            return NotFound();

        return Ok(team);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGameTeam(int id, CancellationToken ct)
    {
        var deleted = await gameTeamService.DeleteAsync(id, ct);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
