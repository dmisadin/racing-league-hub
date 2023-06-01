using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Keyless]
[Table("Race")]
public partial class Race
{
    [Column("FK_Race_TeamId")]
    public byte FkRaceTeamId { get; set; }

    [Column("FK_Race_GrandPrixId")]
    public int FkRaceGrandPrixId { get; set; }

    [Column("FK_Race_DriverId")]
    public int FkRaceDriverId { get; set; }

    public byte Position { get; set; }

    [Column(TypeName = "decimal(8, 3)")]
    public decimal? RaceTime { get; set; }

    public byte ResultStatus { get; set; }

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

    [ForeignKey("FkRaceDriverId")]
    public virtual Driver FkRaceDriver { get; set; } = null!;

    [ForeignKey("FkRaceGrandPrixId")]
    public virtual GrandPrix FkRaceGrandPrix { get; set; } = null!;

    [ForeignKey("FkRaceTeamId")]
    public virtual Team FkRaceTeam { get; set; } = null!;
}
