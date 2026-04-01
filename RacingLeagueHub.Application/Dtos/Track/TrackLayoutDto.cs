using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Models.Enums;

namespace RacingLeagueHub.Application.Dtos.Track;

public class TrackLayoutDto : BaseDto
{
    public EncryptedId TrackId { get; set; }
    public string Name { get; set; }
    public short? PitStopDuration { get; set; }
    public short CornersTotal { get; set; }
    public short CornersLeft { get; set; }
    public short LapsGrandPrix { get; set; }
    public decimal? ElevationChange { get; set; }
    public short Length { get; set; }
    public short TelemetryId { get; set; }

    public virtual List<Game> TrackLayoutGames { get; set; } = new List<Game>();
}
