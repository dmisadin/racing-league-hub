using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Table("League")]
public partial class League
{
    [Key]
    [Column("PK_LeagueId")]
    public int PkLeagueId { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? ImagePath { get; set; }

    [InverseProperty("FkSeasonLeague")]
    public virtual ICollection<Season> Seasons { get; set; } = new List<Season>();
}
