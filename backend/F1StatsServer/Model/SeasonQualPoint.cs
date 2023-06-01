using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Keyless]
public partial class SeasonQualPoint
{
    [Column("FK_SeasonQualPoints_SeasonId")]
    public int FkSeasonQualPointsSeasonId { get; set; }

    public byte Position { get; set; }

    public byte Points { get; set; }

    [ForeignKey("FkSeasonQualPointsSeasonId")]
    public virtual Season FkSeasonQualPointsSeason { get; set; } = null!;
}
