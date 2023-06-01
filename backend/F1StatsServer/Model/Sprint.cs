using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Keyless]
[Table("Sprint")]
public partial class Sprint
{
    [Column("FK_Sprint_TeamId")]
    public byte FkSprintTeamId { get; set; }

    [Column("FK_SprintDriver_GrandPrixId")]
    public int FkSprintDriverGrandPrixId { get; set; }

    [Column("FK_SprintDriver_DriverId")]
    public int FkSprintDriverDriverId { get; set; }

    public byte Position { get; set; }

    [Column(TypeName = "decimal(8, 3)")]
    public decimal RaceTime { get; set; }

    public byte TimePenalty { get; set; }

    public byte LapsCompleted { get; set; }

    public byte GridPosition { get; set; }

    public byte PointsGained { get; set; }

    public bool IsReserve { get; set; }

    [StringLength(8)]
    [Unicode(false)]
    public string? UsedTyres { get; set; }

    [ForeignKey("FkSprintDriverDriverId")]
    public virtual Driver FkSprintDriverDriver { get; set; } = null!;

    [ForeignKey("FkSprintDriverGrandPrixId")]
    public virtual GrandPrix FkSprintDriverGrandPrix { get; set; } = null!;

    [ForeignKey("FkSprintTeamId")]
    public virtual Team FkSprintTeam { get; set; } = null!;
}
