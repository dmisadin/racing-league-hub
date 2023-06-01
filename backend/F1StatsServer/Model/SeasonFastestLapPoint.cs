using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Keyless]
public partial class SeasonFastestLapPoint
{
    [Column("FK_SeasonRacePoints_SeasonId")]
    public int FkSeasonRacePointsSeasonId { get; set; }

    public byte Points { get; set; }

    public byte FinishInsideTopN { get; set; }

    [ForeignKey("FkSeasonRacePointsSeasonId")]
    public virtual Season FkSeasonRacePointsSeason { get; set; } = null!;
}
