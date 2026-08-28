using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Services.TeamService.Dtos;
using RacingLeagueHub.Application.Services.TeamService.Persistence;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.Persistence.Teams;

internal class TeamQueries : ITeamQueries
{
    private readonly RacingContext racingContext;
    private readonly IDtoMapper<Team, TeamDto> mapper;

    public TeamQueries(
        RacingContext racingContext,
        IDtoMapper<Team, TeamDto> mapper)
    {
        this.racingContext = racingContext;
        this.mapper = mapper;
    }

    public async Task<TeamDto?> GetByIdAsync(long id, CancellationToken ct)
    {
        return await racingContext.Team
            .Where(t => t.Id == id)
            .Select(mapper.ToDtoExpression())
            .SingleOrDefaultAsync(ct);
    }

    public async Task<PagedResult<TeamDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct)
    {
        if (page < 1)
            throw new ArgumentOutOfRangeException(nameof(page));

        if (pageSize is < 1 or > 100)
            throw new ArgumentOutOfRangeException(nameof(pageSize));

        var query = racingContext.Team
            .OrderBy(t => t.Name)
            .ThenBy(t => t.Id)
            .Select(mapper.ToDtoExpression());

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<TeamDto>(
            items,
            page,
            pageSize,
            totalCount);
    }
}
