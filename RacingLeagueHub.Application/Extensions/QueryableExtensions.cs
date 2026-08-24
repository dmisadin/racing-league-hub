using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.Models;

namespace RacingLeagueHub.Application.Extensions;

public static class QueryableExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        int page,
        int pageSize = 10,
        CancellationToken ct = default)
    {
        if (page < 1)
            throw new ArgumentOutOfRangeException(nameof(page));

        if (pageSize < 1)
            throw new ArgumentOutOfRangeException(nameof(pageSize));

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<T>(
            items,
            page,
            pageSize,
            totalCount);
    }
}