using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Services.GameTeamService.Dtos;
using RacingLeagueHub.Application.Services.GameTeamService.Persistence;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.Persistence.GameTeams;

internal class GameTeamQueries : IGameTeamQueries
{
    private readonly RacingContext racingContext;
    private readonly IDtoMapper<GameTeam, GameTeamDto> mapper;

    public GameTeamQueries(
        RacingContext racingContext,
        IDtoMapper<GameTeam, GameTeamDto> mapper)
    {
        this.racingContext = racingContext;
        this.mapper = mapper;
    }

    public async Task<GameTeamDto?> GetByIdAsync(long id, CancellationToken ct)
    {
        return await racingContext.GameTeam
            .Where(t => t.Id == id)
            .Select(mapper.ToDtoExpression())
            .SingleOrDefaultAsync(ct);
    }

    public async Task<PagedResult<GameTeamDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct)
    {
        if (page < 1)
            throw new ArgumentOutOfRangeException(nameof(page));

        if (pageSize is < 1 or > 100)
            throw new ArgumentOutOfRangeException(nameof(pageSize));

        var query = racingContext.GameTeam
            .OrderBy(t => t.Name)
            .ThenBy(t => t.Id)
            .Select(mapper.ToDtoExpression());

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<GameTeamDto>(
            items,
            page,
            pageSize,
            totalCount);
    }
}
