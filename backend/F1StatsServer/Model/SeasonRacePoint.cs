using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Keyless]
public partial class SeasonRacePoint
{
    [Column("FK_SeasonRacePoints_SeasonId")]
    public int FkSeasonRacePointsSeasonId { get; set; }

    public byte Position { get; set; }

    public byte Points { get; set; }

    [ForeignKey("FkSeasonRacePointsSeasonId")]
    public virtual Season FkSeasonRacePointsSeason { get; set; } = null!;
}
