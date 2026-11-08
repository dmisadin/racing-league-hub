using RacingLeagueHub.Domain.Common.Models.Enums;
using System.Linq.Expressions;

namespace RacingLeagueHub.Domain.Leagues.Seasons.Rules;

public static class SeasonStatusRules
{
    public static SeasonStatus FromRounds(int roundsTotal, int roundsCompleted)
    {
        if (roundsTotal == 0 || roundsCompleted == 0)
            return SeasonStatus.Upcoming;

        return roundsCompleted < roundsTotal
            ? SeasonStatus.Ongoing
            : SeasonStatus.Completed;
    }

    public static int ProgressPercent(int roundsTotal, int roundsCompleted)
    {
        return roundsTotal == 0
            ? 0
            : (int)Math.Round(roundsCompleted * 100.0 / roundsTotal);
    }

    public static Expression<Func<Season, bool>> HasStatus(SeasonStatus status, DateTimeOffset now)
    {
        return status switch
        {
            SeasonStatus.Upcoming => s => !s.GrandsPrix.Any(gp => gp.StartingAt <= now),
            SeasonStatus.Ongoing => s => s.GrandsPrix.Any(gp => gp.StartingAt <= now)
                                        && s.GrandsPrix.Any(gp => gp.StartingAt > now),
            SeasonStatus.Completed => s => s.GrandsPrix.Any()
                                        && s.GrandsPrix.All(gp => gp.StartingAt <= now),
            _ => throw new ArgumentOutOfRangeException(nameof(status))
        };
    }

    /// <summary>Upcoming or ongoing.</summary>
    public static Expression<Func<Season, bool>> IsActive(DateTimeOffset now)
    {
        return s => !s.GrandsPrix.Any() || s.GrandsPrix.Any(gp => gp.StartingAt > now);
    }
}
