using RacingLeagueHub.Entities.Seasons;
using RacingLeagueHub.Entities.Stewarding;

namespace RacingLeagueHub.Entities;

public class GrandPrix : EntityBase
{
    public long TrackLayoutId { get; set; }
    public long SeasonId { get; set; }
    public string Name { get; set; }
    public DateTimeOffset StartingAt { get; set; }
    public string? VodUrl { get; set; }
    public string Slug { get; set; }

    public virtual TrackLayout TrackLayout { get; set; }
    public virtual Season Season { get; set; }

    public virtual ICollection<Incident> Incidents { get; set; }
}
