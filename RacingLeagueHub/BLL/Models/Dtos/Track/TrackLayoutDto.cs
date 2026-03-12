using RacingLeagueHub.BLL.Models.Enums;

namespace RacingLeagueHub.BLL.Models.Dtos.Track;

public class TrackLayoutDto : BaseDto
{
    public long TrackId { get; set; }
    public string Name { get; set; }
    public short? PitStopDuration { get; set; }
    public short CornersTotal { get; set; }
    public short CornersLeft { get; set; }
    public short LapsGrandPrix { get; set; }

    public virtual List<Game> TrackLayoutGames { get; set; } = new List<Game>();
}
