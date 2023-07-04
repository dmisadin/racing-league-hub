using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Table("Qualifying")]
public partial class Qualifying
{
    [Key]
    [Column("PK_QualifyingId")]
    public int PkQualifyingId { get; set; }

    [Column("FK_Qualifying_TeamId")]
    public byte FkQualifyingTeamId { get; set; }

    [Column("FK_Qualifying_GrandPrixId")]
    public int FkQualifyingGrandPrixId { get; set; }

    [Column("FK_Qualifying_DriverId")]
    public int FkQualifyingDriverId { get; set; }

    public byte Position { get; set; }

    [Column("FastestLapInMS")]
    public int FastestLapInMs { get; set; }

    public byte ResultStatus { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? BestTimeTyre { get; set; }

    public byte PointsGained { get; set; }

    public bool IsReserve { get; set; }

    [ForeignKey("FkQualifyingDriverId")]
    [InverseProperty("Qualifyings")]
    public virtual Driver FkQualifyingDriver { get; set; } = null!;

    [ForeignKey("FkQualifyingGrandPrixId")]
    [InverseProperty("Qualifyings")]
    public virtual GrandPrix FkQualifyingGrandPrix { get; set; } = null!;

    [ForeignKey("FkQualifyingTeamId")]
    [InverseProperty("Qualifyings")]
    public virtual Team FkQualifyingTeam { get; set; } = null!;
}
