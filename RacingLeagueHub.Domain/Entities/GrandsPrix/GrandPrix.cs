using RacingLeagueHub.Domain.Entities.Seasons;
using RacingLeagueHub.Domain.Entities.Stewarding;

namespace RacingLeagueHub.Domain.Entities.GrandsPrix;

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
