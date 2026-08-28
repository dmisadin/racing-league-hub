using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Services.TeamService.Dtos;
using RacingLeagueHub.Application.Services.TeamService.Persistence;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.Persistence.Teams;

internal class TeamCommands : ITeamCommands
{
    private readonly RacingContext racingContext;
    private readonly IDtoMapper<Team, TeamDto> mapper;

    public TeamCommands(
        RacingContext racingContext,
        IDtoMapper<Team, TeamDto> mapper)
    {
        this.racingContext = racingContext;
        this.mapper = mapper;
    }

    public async Task<TeamDto> AddAsync(CreateTeamDto dto, CancellationToken ct)
    {
        var team = new Team
        {
            Name = dto.Name
        };

        racingContext.Team.Add(team);

        await racingContext.SaveChangesAsync(ct);

        return mapper.ToDto(team);
    }

    public async Task<TeamDto?> UpdateAsync(long id, UpdateTeamDto dto, CancellationToken ct)
    {
        var team = await racingContext.Team
            .SingleOrDefaultAsync(
                t => t.Id == id,
                ct);

        if (team is null)
            return null;

        team.Name = dto.Name;

        await racingContext.SaveChangesAsync(ct);

        return mapper.ToDto(team);
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken ct)
    {
        var team = await racingContext.Team
            .SingleOrDefaultAsync(
                t => t.Id == id,
                ct);

        if (team is null)
            return false;

        racingContext.Team.Remove(team);

        await racingContext.SaveChangesAsync(ct);

        return true;
    }
}
