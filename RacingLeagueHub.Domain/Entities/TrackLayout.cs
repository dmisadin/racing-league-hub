using RacingLeagueHub.Domain.Entities.Resources;
using RacingLeagueHub.Domain.Models.Enums;

namespace RacingLeagueHub.Domain.Entities;

public class TrackLayout : EntityBase
{
    public long TrackId { get; set; }
    public string Name { get; set; }
    public short? PitStopDuration { get; set; }
    public short CornersTotal { get; set; }
    public short CornersLeft { get; set; }
    public short LapsGrandPrix { get; set; }
    public decimal? ElevationChange { get; set; }
    public short Length { get; set; }
    public short TelemetryId { get; set; }
    public long? MapImageResourceId { get; set; }
    public long? CoverImageResourceId { get; set; }

    public virtual Track Track { get; set; }
    public virtual Resource? MapImageResource { get; set; }
    public virtual Resource? CoverImageResource { get; set; }

    public virtual ICollection<TrackLayoutGame> TrackLayoutGames { get; set; } = new List<TrackLayoutGame>();
}
