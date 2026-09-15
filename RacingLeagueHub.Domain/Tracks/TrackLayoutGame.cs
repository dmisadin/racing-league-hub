using RacingLeagueHub.Domain.Models.Enums;

namespace RacingLeagueHub.Domain.Tracks;

public class TrackLayoutGame
{
    public int TrackLayoutId { get; set; }
    public Game Game { get; set; }

    public virtual TrackLayout TrackLayout { get; set; }
}
