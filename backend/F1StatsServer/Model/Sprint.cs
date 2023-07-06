using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Table("Sprint")]
public partial class Sprint : EntityBase
{
    [Key]
    [Column("PK_SprintId")]
    public int PkSprintId { get; set; }

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
    [InverseProperty("Sprints")]
    public virtual Driver FkSprintDriverDriver { get; set; } = null!;

    [ForeignKey("FkSprintDriverGrandPrixId")]
    [InverseProperty("Sprints")]
    public virtual GrandPrix FkSprintDriverGrandPrix { get; set; } = null!;

    [ForeignKey("FkSprintTeamId")]
    [InverseProperty("Sprints")]
    public virtual Team FkSprintTeam { get; set; } = null!;
}
