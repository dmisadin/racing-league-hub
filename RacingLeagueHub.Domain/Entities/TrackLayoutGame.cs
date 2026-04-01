using RacingLeagueHub.Domain.Models.Enums;

namespace RacingLeagueHub.Domain.Entities;

public class TrackLayoutGame
{
    public long TrackLayoutId { get; set; }
    public Game Game { get; set; }

    public virtual TrackLayout TrackLayout { get; set; }
}
