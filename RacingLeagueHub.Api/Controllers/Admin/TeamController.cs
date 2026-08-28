using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Api.Authorization;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Services.TeamService;
using RacingLeagueHub.Application.Services.TeamService.Dtos;

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
    public async Task<ActionResult<TeamDto>> GetById(EncryptedId id, CancellationToken ct)
    {
        var team = await teamService.GetByIdAsync(id.RawId, ct);

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
    public async Task<ActionResult<TeamDto>> UpdateTeam([FromRoute] EncryptedId id, [FromBody] UpdateTeamDto dto, CancellationToken ct)
    {
        var team = await teamService.UpdateAsync(id.RawId, dto, ct);

        if (team is null)
            return NotFound();

        return Ok(team);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeam(EncryptedId id, CancellationToken ct)
    {
        var deleted = await teamService.DeleteAsync(id.RawId, ct);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
