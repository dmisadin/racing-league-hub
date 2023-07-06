using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Table("League")]
public partial class League : EntityBase
{
    [Key]
    [Column("PK_LeagueId")]
    public int PkLeagueId { get; set; }

    [Column("FK_League_UserId")]
    public int FkLeagueUserId { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? ImagePath { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Description { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string ColorHex { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? Website { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Discord { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? YouTube { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Twitch { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Facebook { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Instagram { get; set; }

    [ForeignKey("FkLeagueUserId")]
    [InverseProperty("Leagues")]
    public virtual User FkLeagueUser { get; set; } = null!;

    [InverseProperty("FkSeasonLeague")]
    public virtual ICollection<Season> Seasons { get; set; } = new List<Season>();

    [ForeignKey("FkLeagueUserLeagueId")]
    [InverseProperty("FkLeagueUserLeagues")]
    public virtual ICollection<User> FkLeagueUserUsers { get; set; } = new List<User>();
}
