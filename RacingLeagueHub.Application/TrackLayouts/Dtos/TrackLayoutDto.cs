using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Models.Enums;

namespace RacingLeagueHub.Application.TrackLayouts.Dtos;

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

public class CreateTrackLayoutDto
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

    public EncryptedId? MapImageResourceId { get; set; }
    public EncryptedId? CoverImageResourceId { get; set; }

    public virtual List<Game> TrackLayoutGames { get; set; } = new List<Game>();
}

public class UpdateTrackLayoutDto
{
    public string Name { get; set; }
    public short? PitStopDuration { get; set; }
    public short CornersTotal { get; set; }
    public short CornersLeft { get; set; }
    public short LapsGrandPrix { get; set; }
    public decimal? ElevationChange { get; set; }
    public short Length { get; set; }
    public short TelemetryId { get; set; }

    public EncryptedId? MapImageResourceId { get; set; }
    public EncryptedId? CoverImageResourceId { get; set; }

    public virtual List<Game> TrackLayoutGames { get; set; } = new List<Game>();
}