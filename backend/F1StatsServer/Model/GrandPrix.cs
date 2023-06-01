using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Table("GrandPrix")]
public partial class GrandPrix
{
    [Key]
    [Column("PK_GrandPrixId")]
    public int PkGrandPrixId { get; set; }

    [Column("FK_GrandPrix_SeasonId")]
    public int FkGrandPrixSeasonId { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "smalldatetime")]
    public DateTime StartTime { get; set; }

    public bool HasSprint { get; set; }

    [ForeignKey("FkGrandPrixSeasonId")]
    [InverseProperty("GrandPrixes")]
    public virtual Season FkGrandPrixSeason { get; set; } = null!;
}
