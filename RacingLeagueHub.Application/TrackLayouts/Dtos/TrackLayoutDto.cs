using RacingLeagueHub.Application.Common.Dtos;
using RacingLeagueHub.Domain.Common.Models.Enums;

namespace RacingLeagueHub.Application.TrackLayouts.Dtos;

public class TrackLayoutDto : BaseDto
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

    public virtual List<Game> TrackLayoutGames { get; set; } = new List<Game>();
}

public class CreateTrackLayoutDto
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

    public int? MapImageResourceId { get; set; }
    public int? CoverImageResourceId { get; set; }

    public virtual List<Game> TrackLayoutGames { get; set; } = new List<Game>();
}