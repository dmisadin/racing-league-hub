using RacingLeagueHub.Domain.Common.Entitites;
using RacingLeagueHub.Domain.Common.Models.Enums;
using RacingLeagueHub.Domain.Leagues;
using RacingLeagueHub.Domain.Resources;

namespace RacingLeagueHub.Domain.Leagues.Seasons;

public class Season : EntityBase
{
    public int LeagueId { get; set; }
    public string Name { get; set; }
    public Platform Platform { get; set; }
    public Game Game { get; set; }
    public short LapPercentageRequired { get; set; }
    public string Slug { get; set; }
    public int? LogoResourceId { get; set; }

    public virtual League League { get; set; }
    public virtual Resource? LogoResource { get; set; }

    public virtual ICollection<SeasonDivision> SeasonDivisions { get; set; }
}
