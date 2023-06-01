using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Keyless]
public partial class SeasonSprintPoint
{
    [Column("FK_SeasonSprintPoints_SeasonId")]
    public int FkSeasonSprintPointsSeasonId { get; set; }

    public byte Position { get; set; }

    public byte Points { get; set; }

    [ForeignKey("FkSeasonSprintPointsSeasonId")]
    public virtual Season FkSeasonSprintPointsSeason { get; set; } = null!;
}
