using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Keyless]
[Table("SeasonDriver")]
public partial class SeasonDriver
{
    [Column("FK_SeasonDriver_DriverId")]
    public int FkSeasonDriverDriverId { get; set; }

    [Column("FK_SeasonDriver_SeasonId")]
    public int FkSeasonDriverSeasonId { get; set; }

    [Column("FK_SeasonDriver_TeamId")]
    public byte FkSeasonDriverTeamId { get; set; }

    public byte RacingNumber { get; set; }

    public byte PenaltyPoints { get; set; }

    [ForeignKey("FkSeasonDriverDriverId")]
    public virtual Driver FkSeasonDriverDriver { get; set; } = null!;

    [ForeignKey("FkSeasonDriverSeasonId")]
    public virtual Season FkSeasonDriverSeason { get; set; } = null!;

    [ForeignKey("FkSeasonDriverTeamId")]
    public virtual Team FkSeasonDriverTeam { get; set; } = null!;
}
