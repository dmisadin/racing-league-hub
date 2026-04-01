using RacingLeagueHub.Domain.Models.Enums;

namespace RacingLeagueHub.Domain.Entities.Seasons;

public class SeasonPoints : EntityBase
{
    public long SeasonId { get; set; }
    public SessionType SessionType { get; set; }
    public short Position { get; set; }
    public short Points { get; set; }

    public virtual Season Season { get; set; }
}