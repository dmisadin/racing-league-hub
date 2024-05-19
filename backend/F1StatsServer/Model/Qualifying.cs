using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Model.Enums;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Table("Qualifying")]
public partial class Qualifying : EntityBase
{
    public int TeamId { get; set; }

    public int GrandPrixId { get; set; }

    public int DriverId { get; set; }

    public byte Position { get; set; }

    [Column("FastestLapInMS")]
    public int FastestLapInMs { get; set; }

    public ResultStatus ResultStatus { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? BestTimeTyre { get; set; }

    public byte PointsGained { get; set; }

    public bool IsReserve { get; set; }

    [ForeignKey("DriverId")]
    [InverseProperty("Qualifyings")]
    public virtual Driver Driver { get; set; } = null!;

    [ForeignKey("GrandPrixId")]
    [InverseProperty("Qualifyings")]
    public virtual GrandPrix GrandPrix { get; set; } = null!;

    [ForeignKey("TeamId")]
    [InverseProperty("Qualifyings")]
    public virtual Team Team { get; set; } = null!;
}
