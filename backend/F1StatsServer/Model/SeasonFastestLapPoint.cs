using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

public partial class SeasonFastestLapPoint : EntityBase
{
    public byte Points { get; set; }

    public byte Position { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("SeasonFastestLapPoint")]
    public virtual Season Season { get; set; } = null!;
}
