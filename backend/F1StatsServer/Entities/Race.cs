using F1StatsServer.Entities.Enums;
using F1StatsServer.Utility;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1StatsServer.Entities;

[Table("Race")]
public partial class Race : EntityBase
{
    public int TeamId { get; set; }

    public int GrandPrixId { get; set; }

    public int DriverId { get; set; }

    public byte Position { get; set; }

    [Column(TypeName = "decimal(8, 3)")]
    public decimal? RaceTime { get; set; }

    public ResultStatus ResultStatus { get; set; }

    public byte TimePenalty { get; set; }

    public short PostRaceTimePenalty { get; set; }

    public byte LapsCompleted { get; set; }

    public byte GridPosition { get; set; }

    [Column("FastestLapInMS")]
    public int? FastestLapInMs { get; set; }

    public byte PointsGained { get; set; }

    public bool IsReserve { get; set; }

    [StringLength(8)]
    [Unicode(false)]
    public string? UsedTyres { get; set; }

    [ForeignKey("DriverId")]
    [InverseProperty("Races")]
    public virtual Driver Driver { get; set; } = null!;

    [ForeignKey("GrandPrixId")]
    [InverseProperty("Races")]
    public virtual GrandPrix GrandPrix { get; set; } = null!;

    [ForeignKey("TeamId")]
    [InverseProperty("Races")]
    public virtual Team Team { get; set; } = null!;
}
