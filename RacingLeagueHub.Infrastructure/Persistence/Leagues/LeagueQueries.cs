using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.Common.Mappers;
using RacingLeagueHub.Application.Leagues.Dtos;
using RacingLeagueHub.Application.Leagues.Persistence;
using RacingLeagueHub.Domain.Common.Models;
using RacingLeagueHub.Domain.Leagues;
using RacingLeagueHub.Infrastructure.Extensions;

namespace RacingLeagueHub.Infrastructure.Persistence.Leagues;

internal class LeagueQueries : ILeagueQueries
{
    private readonly RacingContext racingContext;
    private readonly IDtoMapper<League, LeagueDto> mapper;

    public LeagueQueries(RacingContext racingContext,
        IDtoMapper<League, LeagueDto> mapper)
    {
        this.racingContext = racingContext;
        this.mapper = mapper;
    }

    public async Task<LeagueDto?> GetBySlugAsync(string leagueSlug, CancellationToken ct = default)
    {
        return await racingContext.League
            .Where(l => l.Slug == leagueSlug)
            .Select(mapper.ToDtoExpression())
            .FirstOrDefaultAsync(ct);
    }

    public async Task<PagedResult<LeagueDto>> GetLeaguesAsync(
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default)
    {
        return await racingContext.League
            .Select(mapper.ToDtoExpression())
            .ToPagedResultAsync(page, pageSize, ct);
    }
}
