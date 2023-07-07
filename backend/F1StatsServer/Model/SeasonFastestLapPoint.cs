using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

public partial class SeasonFastestLapPoint : EntityBase
{

    public int SeasonId { get; set; }

    public byte Points { get; set; }

    public byte FinishInsideTopN { get; set; }

    [ForeignKey("SeasonId")]
    [InverseProperty("SeasonFastestLapPoints")]
    public virtual Season Season { get; set; } = null!;
}
