using RacingLeagueHub.Domain.GrandsPrix;

namespace RacingLeagueHub.Infrastructure.Persistence.GrandsPrix;

internal static class GrandPrixScopeExtensions
{
    /// <summary>
    /// Restricts Grands Prix to a season and, when <paramref name="divisionSlug"/> is given,
    /// to that division's calendar within the season.
    /// </summary>
    public static IQueryable<GrandPrix> InScope(
        this IQueryable<GrandPrix> query,
        string leagueSlug,
        string seasonSlug,
        string? divisionSlug)
    {
        query = query.Where(gp =>
            gp.Season.Slug == seasonSlug &&
            gp.Season.League.Slug == leagueSlug);

        return divisionSlug is null
            ? query
            : query.Where(gp => gp.SeasonDivision != null && gp.SeasonDivision.Division.Slug == divisionSlug);
    }
}
