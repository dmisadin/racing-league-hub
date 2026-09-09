using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Seasons.Dtos;
using RacingLeagueHub.Application.Seasons.Persistence;
using RacingLeagueHub.Domain.Entities.Seasons;
using RacingLeagueHub.Infrastructure.Extensions;
using RacingLeagueHub.Infrastructure.Persistence;

namespace RacingLeagueHub.Infrastructure.Persistence.Seasons;

internal class SeasonQueries : ISeasonQueries
{
    private readonly RacingContext racingContext;
    private readonly IDtoMapper<Season, SeasonDto> mapper;

    public SeasonQueries(RacingContext racingContext,
        IDtoMapper<Season, SeasonDto> mapper)
    {
        this.racingContext = racingContext;
        this.mapper = mapper;
    }

    public async Task<SeasonDto?> GetBySlugAsync(
        string leagueSlug,
        string seasonSlug,
        CancellationToken ct = default)
    {
        return await racingContext.Season
            .Where(s => s.Slug == seasonSlug && s.League.Slug == leagueSlug)
            .Select(mapper.ToDtoExpression())
            .FirstOrDefaultAsync(ct);
    }

    public async Task<PagedResult<SeasonDto>> GetLeagueSeasonsAsync(
        string leagueSlug, 
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default)
    {
        return await racingContext.Season
            .Where(s => s.League.Slug == leagueSlug)
            .Select(mapper.ToDtoExpression())
            .ToPagedResultAsync(page, pageSize, ct);
    }

    public async Task<PagedResult<SeasonDto>> GetLeagueSeasonsAsync(
        long leagueId,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default)
    {
        return await racingContext.Season
            .Where(s => s.LeagueId == leagueId)
            .Select(mapper.ToDtoExpression())
            .ToPagedResultAsync(page, pageSize, ct);
    }
}
