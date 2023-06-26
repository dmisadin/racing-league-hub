using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Keyless]
public partial class SeasonAssist
{
    [Column("FK_SeasonAssists_SeasonId")]
    public int FkSeasonAssistsSeasonId { get; set; }

    [StringLength(16)]
    [Unicode(false)]
    public string RacingLine { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string Gearbox { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string TractionControl { get; set; } = null!;

    [Column("ABS")]
    public bool Abs { get; set; }

    [ForeignKey("FkSeasonAssistsSeasonId")]
    public virtual Season FkSeasonAssistsSeason { get; set; } = null!;
}
