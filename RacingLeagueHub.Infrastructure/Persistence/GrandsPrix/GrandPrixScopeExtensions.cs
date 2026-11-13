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
            gp.SeasonDivision.Season.Slug == seasonSlug &&
            gp.SeasonDivision.Season.League.Slug == leagueSlug);

        return divisionSlug is null
            ? query
            : query.Where(gp => gp.SeasonDivision.Division.Slug == divisionSlug);
    }

    /// <summary>
    /// A single Grand Prix by slug. Slugs are unique per division, so without a division the slug
    /// can match one Grand Prix per division: the top-tier division's comes first.
    /// </summary>
    public static IQueryable<GrandPrix> BySlug(
        this IQueryable<GrandPrix> query,
        string leagueSlug,
        string seasonSlug,
        string? divisionSlug,
        string grandPrixSlug)
    {
        return query
            .InScope(leagueSlug, seasonSlug, divisionSlug)
            .Where(gp => gp.Slug == grandPrixSlug)
            .OrderBy(gp => gp.SeasonDivision.Division.Tier)
            .ThenBy(gp => gp.SeasonDivisionId);
    }
}
