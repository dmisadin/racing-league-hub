using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

public partial class SeasonAssist : EntityBase
{
    public int SeasonId { get; set; }

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

    [ForeignKey("SeasonId")]
    [InverseProperty("SeasonAssists")]
    public virtual Season Season { get; set; } = null!;
}
