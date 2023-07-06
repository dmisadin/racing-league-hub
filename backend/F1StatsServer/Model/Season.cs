using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Table("Season")]
public partial class Season : EntityBase
{
    [Key]
    [Column("PK_SeasonId")]
    public int PkSeasonId { get; set; }

    [Column("FK_Season_LeagueId")]
    public int FkSeasonLeagueId { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string Game { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? ImagePath { get; set; }

    public byte LapsRequiredPercentage { get; set; }

    [ForeignKey("FkSeasonLeagueId")]
    [InverseProperty("Seasons")]
    public virtual League FkSeasonLeague { get; set; } = null!;

    [InverseProperty("FkGrandPrixSeason")]
    public virtual ICollection<GrandPrix> GrandPrixes { get; set; } = new List<GrandPrix>();
}
