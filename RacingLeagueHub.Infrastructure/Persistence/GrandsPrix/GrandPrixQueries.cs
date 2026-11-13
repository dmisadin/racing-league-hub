using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.Common.Mappers;
using RacingLeagueHub.Application.GrandsPrix.Dtos;
using RacingLeagueHub.Application.GrandsPrix.Persistence;
using RacingLeagueHub.Domain.Common.Models;
using RacingLeagueHub.Domain.GrandsPrix;
using RacingLeagueHub.Infrastructure.Extensions;

namespace RacingLeagueHub.Infrastructure.Persistence.GrandsPrix;

internal class GrandPrixQueries : IGrandPrixQueries
{
    private readonly RacingContext racingContext;
    private readonly IDtoMapper<GrandPrix, GrandPrixDto> mapper;

    public GrandPrixQueries(RacingContext racingContext,
        IDtoMapper<GrandPrix, GrandPrixDto> mapper)
    {
        this.racingContext = racingContext;
        this.mapper = mapper;
    }

    public async Task<GrandPrixDto?> GetByIdAsync(
        int id,
        CancellationToken ct = default)
    {
        return await racingContext.GrandPrix
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(mapper.ToDtoExpression())
            .FirstOrDefaultAsync(ct);
    }

    public async Task<GrandPrixDto?> GetBySlugAsync(
        string leagueSlug,
        string seasonSlug,
        string? divisionSlug,
        string grandPrixSlug,
        CancellationToken ct = default)
    {
        return await racingContext.GrandPrix
            .AsNoTracking()
            .BySlug(leagueSlug, seasonSlug, divisionSlug, grandPrixSlug)
            .Select(mapper.ToDtoExpression())
            .FirstOrDefaultAsync(ct);
    }

    public async Task<PagedResult<GrandPrixDto>> GetSeasonGrandsPrixAsync(
        string leagueSlug,
        string seasonSlug,
        string? divisionSlug,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default)
    {
        return await racingContext.GrandPrix
            .AsNoTracking()
            .InScope(leagueSlug, seasonSlug, divisionSlug)
            .OrderBy(x => x.StartingAt)
            .ThenBy(x => x.Id)
            .Select(mapper.ToDtoExpression())
            .ToPagedResultAsync(page, pageSize, ct);
    }
}
