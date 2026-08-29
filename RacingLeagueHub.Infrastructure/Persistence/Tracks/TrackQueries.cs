using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.DtoMappers;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Services.TrackService.Dtos;
using RacingLeagueHub.Application.Services.TrackService.Persistence;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.Persistence.Teams;

internal class TrackQueries : ITrackQueries
{
    private readonly RacingContext racingContext;
    private readonly IDtoMapper<Track, TrackDto> mapper;

    public TrackQueries(
        RacingContext racingContext,
        IDtoMapper<Track, TrackDto> mapper)
    {
        this.racingContext = racingContext;
        this.mapper = mapper;
    }

    public async Task<TrackDto?> GetByIdAsync(long id, CancellationToken ct)
    {
        return await racingContext.Track
            .Where(t => t.Id == id)
            .Select(mapper.ToDtoExpression())
            .SingleOrDefaultAsync(ct);
    }

    public async Task<PagedResult<TrackDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct)
    {
        if (page < 1)
            throw new ArgumentOutOfRangeException(nameof(page));

        if (pageSize is < 1 or > 100)
            throw new ArgumentOutOfRangeException(nameof(pageSize));

        var query = racingContext.Track
            .OrderBy(t => t.Name)
            .ThenBy(t => t.Id)
            .Select(mapper.ToDtoExpression());

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<TrackDto>(
            items,
            page,
            pageSize,
            totalCount);
    }
}
