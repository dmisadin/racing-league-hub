using F1StatsServer.Entities.Enums;
using F1StatsServer.Utility;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1StatsServer.Entities
{
    [Table("SessionResult")]
    public class SessionResult : EntityBase
    {
        public SessionType SessionType { get; set; }
        public int GrandPrixId { get; set; }
        public int TeamId { get; set; }
        public int DriverId { get; set; }
        public int Position { get; set; }
        public decimal? RaceTime { get; set; }
        public int? FastestLapInMs { get; set; }
        public ResultStatus ResultStatus { get; set; }
        public int? TimePenalty { get; set; }
        public int? PostRaceTimePenalty { get; set; }
        public int? LapsCompleted {  get; set; }
        public int? GridPosition { get; set; }
        public int? PointsGained { get; set; }
        public string? UsedTyres { get; set; }
        public string? BestTimeTyre { get; set; }
        public bool IsReserve { get; set; }
        public bool SelectedForDeletion { get; set; }

        [ForeignKey("DriverId")]
        [InverseProperty("SessionResults")]
        public virtual Driver Driver { get; set; } = null!;

        [ForeignKey("GrandPrixId")]
        [InverseProperty("SessionResults")]
        public virtual GrandPrix GrandPrix { get; set; } = null!;

        [ForeignKey("TeamId")]
        [InverseProperty("SessionResults")]
        public virtual Team Team { get; set; } = null!;
    }
}
