using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Keyless]
[Table("Qualifying")]
public partial class Qualifying
{
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
    public virtual Driver FkQualifyingDriver { get; set; } = null!;

    [ForeignKey("FkQualifyingGrandPrixId")]
    public virtual GrandPrix FkQualifyingGrandPrix { get; set; } = null!;

    [ForeignKey("FkQualifyingTeamId")]
    public virtual Team FkQualifyingTeam { get; set; } = null!;
}
