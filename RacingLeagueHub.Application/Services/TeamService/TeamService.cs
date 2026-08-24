using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Extensions;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Services.Infrastructure;
using RacingLeagueHub.Application.Services.TeamService.Dtos;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Application.Services.TeamService;

public class TeamService : ITeamService
{
    private readonly IRacingContext racingContext;
    private readonly IDtoMapper<Team, TeamDto> mapper;

    public TeamService(
        IDtoMapper<Team, TeamDto> mapper, 
        IRacingContext racingContext)
    {
        this.mapper = mapper;
        this.racingContext = racingContext;
    }

    public async Task<TeamDto?> GetByIdAsync(long id, CancellationToken ct)
    {
        return await racingContext.Team
            .Where(x => x.Id == id)
            .Select(mapper.ToDtoExpression())
            .SingleOrDefaultAsync(ct);
    }

    public async Task<PagedResult<TeamDto>> GetPagedAsync(int page, CancellationToken ct)
    {
        return await racingContext.Team
            .Select(mapper.ToDtoExpression())
            .ToPagedResultAsync(page, 10, ct);
    }

    public async Task<TeamDto> AddAsync(CreateTeamDto dto, CancellationToken ct)
    {
        var team = new Team
        {
            Name = dto.Name,
            Color = dto.Color
        };

        racingContext.Team.Add(team);

        await racingContext.SaveChangesAsync(ct);

        return mapper.ToDto(team);
    }

    public async Task<TeamDto?> UpdateAsync(long id, UpdateTeamDto dto, CancellationToken ct)
    {
        var team = await racingContext.Team
            .SingleOrDefaultAsync(x => x.Id == id, ct);

        if (team is null)
            return null;

        team.Name = dto.Name;
        team.Color = dto.Color;

        await racingContext.SaveChangesAsync(ct);

        return mapper.ToDto(team);
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken ct)
    {
        var team = await racingContext.Team
            .SingleOrDefaultAsync(x => x.Id == id, ct);

        if (team is null)
            return false;

        racingContext.Team.Remove(team);

        await racingContext.SaveChangesAsync(ct);

        return true;
    }
}