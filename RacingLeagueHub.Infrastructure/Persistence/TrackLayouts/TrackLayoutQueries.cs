using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.TrackLayouts.Dtos;
using RacingLeagueHub.Application.TrackLayouts.Persistence;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.Persistence.TrackLayouts;

internal class TrackLayoutQueries : ITrackLayoutQueries
{
    private readonly RacingContext racingContext;
    private readonly IDtoMapper<TrackLayout, TrackLayoutDto> mapper;

    public TrackLayoutQueries(RacingContext racingContext,
        IDtoMapper<TrackLayout, TrackLayoutDto> mapper)
    {
        this.racingContext = racingContext;
        this.mapper = mapper;
    }

    public async Task<TrackLayoutDto?> GetByIdAsync(long id, CancellationToken ct)
    {
        return await racingContext.TrackLayout
            .Where(t => t.Id == id)
            .Select(mapper.ToDtoExpression())
            .SingleOrDefaultAsync();
    }

    public async Task<PagedResult<TrackLayoutDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct)
    {
        if (page < 1)
            throw new ArgumentOutOfRangeException(nameof(page));

        if (pageSize is < 1 or > 100)
            throw new ArgumentOutOfRangeException(nameof(pageSize));

        var query = racingContext.TrackLayout
            .OrderBy(t => t.Name)
            .ThenBy(t => t.Id)
            .Select(mapper.ToDtoExpression());

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<TrackLayoutDto>(
            items,
            page,
            pageSize,
            totalCount);
    }
}
