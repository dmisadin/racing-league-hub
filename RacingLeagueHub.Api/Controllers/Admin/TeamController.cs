using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Api.Authorization;
using RacingLeagueHub.Application.Teams;
using RacingLeagueHub.Application.Teams.Dtos;

namespace RacingLeagueHub.Api.Controllers.Admin;

[Authorize(Policy = AppPolicies.SuperAdmin)]
[Route("api/team")]
public class TeamController : ApiController
{
    private readonly ITeamService teamService;

    public TeamController(ITeamService teamService)
    {
        this.teamService = teamService;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<TeamDto>> GetById(int id, CancellationToken ct)
    {
        var team = await teamService.GetByIdAsync(id, ct);

        if (team is null)
            return NotFound();

        return Ok(team);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TeamDto>>> GetPaged([FromQuery] int page = 1, CancellationToken ct = default)
    {
        var teams = await teamService.GetPagedAsync(page, ct);

        return Ok(teams);
    }

    [HttpPost]
    public async Task<ActionResult<TeamDto>> AddTeam([FromBody] CreateTeamDto dto, CancellationToken ct)
    {
        var team = await teamService.AddAsync(dto, ct);

        return CreatedAtAction(nameof(GetById), new { id = team.Id }, team);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TeamDto>> UpdateTeam([FromRoute] int id, [FromBody] UpdateTeamDto dto, CancellationToken ct)
    {
        var team = await teamService.UpdateAsync(id, dto, ct);

        if (team is null)
            return NotFound();

        return Ok(team);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeam(int id, CancellationToken ct)
    {
        var deleted = await teamService.DeleteAsync(id, ct);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
