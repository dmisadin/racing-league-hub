namespace RacingLeagueHub.Application.Models;

public record PagedResult<T>(IReadOnlyList<T> Items,
    int Page,
    int PageSize,
    int TotalCount)
{
    public bool HasMore => Page * PageSize < TotalCount;
}