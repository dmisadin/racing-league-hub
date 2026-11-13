using RacingLeagueHub.Domain.Common.Entitites;
using RacingLeagueHub.Domain.Leagues.Seasons;

namespace RacingLeagueHub.Domain.Leagues.Divisions;

public class Division : EntityBase
{
    public const string DefaultName = "Main";
    public const string DefaultSlug = "main";
    public const short DefaultTier = 1;

    public int LeagueId { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public short Tier { get; set; }

    public virtual League League { get; set; } = null!;
    public virtual ICollection<SeasonDivision> SeasonDivisions { get; set; }

    public static Division CreateDefault() => new()
    {
        Name = DefaultName,
        Slug = DefaultSlug,
        Tier = DefaultTier
    };
}
