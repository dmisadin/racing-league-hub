using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Entities.Enums;
using F1StatsServer.Utility;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Entities;

public partial class SeasonPoints : EntityBase
{
    public int SeasonId { get; set; }
    public byte Position { get; set; }
    public byte Points { get; set; }
    public PointsType PointsType { get; set; }

    [ForeignKey("SeasonId")]
    [InverseProperty("SeasonPoints")]
    public virtual Season Season { get; set; } = null!;
}
