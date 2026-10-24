using RacingLeagueHub.Domain.Common.Entitites;
using RacingLeagueHub.Domain.Common.Models.Enums;

namespace RacingLeagueHub.Domain.GrandsPrix;

public class GrandPrixResult : EntityBase
{
    public int GrandPrixDriverId { get; set; }
    public SessionType SessionType { get; set; }
    public int Position { get; set; }
    public int? GridPosition { get; set; }
    public short ResultStatus { get; set; }
    public int? RaceTimeInMs { get; set; }
    public short TimePenalty { get; set; }
    public int? StewardTimePenalty { get; set; }
    public short LapsCompleted { get; set; }
    public short? PointsGained { get; set; }
    public string? UsedTyres { get; set; }
    public int? FastestLapInMs { get; set; }
    public string? BestTimeTyre { get; set; }

    public virtual GrandPrixDriver GrandPrixDriver { get; set; }
}
