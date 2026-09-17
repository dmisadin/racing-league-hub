using RacingLeagueHub.Domain.Common.Entitites;
using RacingLeagueHub.Domain.Leagues.Seasons;
using RacingLeagueHub.Domain.Stewarding;
using RacingLeagueHub.Domain.Tracks;

namespace RacingLeagueHub.Domain.GrandsPrix;

public class GrandPrix : EntityBase
{
    public int TrackLayoutId { get; set; }
    public int SeasonId { get; set; }
    public string Name { get; set; }
    public DateTimeOffset StartingAt { get; set; }
    public string? VodUrl { get; set; }
    public string Slug { get; set; }

    public virtual TrackLayout TrackLayout { get; set; }
    public virtual Season Season { get; set; }

    public virtual ICollection<Incident> Incidents { get; set; }
}
