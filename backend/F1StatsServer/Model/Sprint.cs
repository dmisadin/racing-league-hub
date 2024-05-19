using F1StatsServer.Model.Enums;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1StatsServer.Model;

[Table("Sprint")]
public partial class Sprint : EntityBase
{
    public int TeamId { get; set; }

    public int GrandPrixId { get; set; }

    public int DriverId { get; set; }

    public byte Position { get; set; }

    [Column(TypeName = "decimal(8, 3)")]
    public decimal RaceTime { get; set; }

    public ResultStatus ResultStatus { get; set; }

    public byte TimePenalty { get; set; }

    public byte LapsCompleted { get; set; }

    public byte GridPosition { get; set; }

    public byte PointsGained { get; set; }

    public bool IsReserve { get; set; }

    [StringLength(8)]
    [Unicode(false)]
    public string? UsedTyres { get; set; }

    [ForeignKey("DriverId")]
    [InverseProperty("Sprints")]
    public virtual Driver Driver { get; set; } = null!;

    [ForeignKey("GrandPrixId")]
    [InverseProperty("Sprints")]
    public virtual GrandPrix GrandPrix { get; set; } = null!;

    [ForeignKey("TeamId")]
    [InverseProperty("Sprints")]
    public virtual Team Team { get; set; } = null!;
}
