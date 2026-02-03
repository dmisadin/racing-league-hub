using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Utility;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Entities;

public partial class SeasonAssists : EntityBase
{
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

    [ForeignKey("Id")]
    [InverseProperty("SeasonAssist")]
    public virtual Season Season { get; set; } = null!;
}
