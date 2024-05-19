using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Utility;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Entities;

[Table("SeasonDriver")]
public partial class SeasonDrivers : EntityBase
{
    public int DriverId { get; set; }

    public int SeasonId { get; set; }

    public int TeamId { get; set; }

    public byte RacingNumber { get; set; }

    public byte PenaltyPoints { get; set; }

    [ForeignKey("DriverId")]
    [InverseProperty("SeasonDrivers")]
    public virtual Driver Driver { get; set; } = null!;

    [ForeignKey("SeasonId")]
    [InverseProperty("SeasonDrivers")]
    public virtual Season Season { get; set; } = null!;

    [ForeignKey("TeamId")]
    [InverseProperty("SeasonDrivers")]
    public virtual Team Team { get; set; } = null!;
}
