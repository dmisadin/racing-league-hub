using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Table("User")]
public partial class User
{
    [Key]
    [Column("PK_UserId")]
    public int PkUserId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    public bool IsAdmin { get; set; }

    [InverseProperty("FkLeagueUser")]
    public virtual ICollection<League> Leagues { get; set; } = new List<League>();

    [ForeignKey("FkLeagueUserUserId")]
    [InverseProperty("FkLeagueUserUsers")]
    public virtual ICollection<League> FkLeagueUserLeagues { get; set; } = new List<League>();
}
