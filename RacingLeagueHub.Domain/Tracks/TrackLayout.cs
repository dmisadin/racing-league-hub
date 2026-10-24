using RacingLeagueHub.Domain.Common.Entitites;
using RacingLeagueHub.Domain.Resources;

namespace RacingLeagueHub.Domain.Tracks;

public class TrackLayout : EntityBase
{
    public int TrackId { get; set; }
    public string Name { get; set; }
    public short? PitStopDuration { get; set; }
    public short CornersTotal { get; set; }
    public short CornersLeft { get; set; }
    public short LapsGrandPrix { get; set; }
    public decimal? ElevationChange { get; set; }
    public short Length { get; set; }
    public short TelemetryId { get; set; }
    public int? MapImageResourceId { get; set; }
    public int? CoverImageResourceId { get; set; }

    public virtual Track Track { get; set; }
    public virtual Resource? MapImageResource { get; set; }
    public virtual Resource? CoverImageResource { get; set; }

    public virtual ICollection<TrackLayoutGame> TrackLayoutGames { get; set; } = new List<TrackLayoutGame>();
}
