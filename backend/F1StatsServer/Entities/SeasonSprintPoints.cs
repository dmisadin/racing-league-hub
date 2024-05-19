using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Utility;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Entities;

public partial class SeasonSprintPoints : EntityBase
{
    public int SeasonId { get; set; }

    public byte Position { get; set; }

    public byte Points { get; set; }

    [ForeignKey("SeasonId")]
    [InverseProperty("SeasonSprintPoints")]
    public virtual Season Season { get; set; } = null!;
}
