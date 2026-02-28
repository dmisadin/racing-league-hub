using RacingLeagueHub.Entities.Enums;

namespace RacingLeagueHub.Entities;

public class GrandPrixResult : EntityBase
{
    public long GrandPrixDriverId { get; set; }
    public SessionType SessionType { get; set; }
    public long Position { get; set; }
    public long? GridPosition { get; set; }
    public short ResultStatus { get; set; }
    public long? RaceTimeInMs { get; set; }
    public short TimePenalty { get; set; }
    public long? StewardTimePenalty { get; set; }
    public short LapsCompleted { get; set; }
    public short? PointsGained { get; set; }
    public string? UsedTyres { get; set; }
    public long? FastestLapInMs { get; set; }
    public string? BestTimeTyre { get; set; }

    public virtual GrandPrixDriver GrandPrixDriver { get; set; }
}
