using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Table("LeagueUser")]
public partial class LeagueUser : EntityBase
{
    public int UserId { get; set; }

    public int LeagueId { get; set; }

    public bool IsOwner { get; set; }

    [ForeignKey("LeagueId")]
    [InverseProperty("LeagueUsers")]
    public virtual League League { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("LeagueUsers")]
    public virtual User User { get; set; } = null!;
}
