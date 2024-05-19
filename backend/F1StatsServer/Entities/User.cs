using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Utility;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Entities;

[Table("User")]
public partial class User : EntityBase
{
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

    [InverseProperty("User")]
    public virtual ICollection<LeagueUser> LeagueUsers { get; set; } = new List<LeagueUser>();
}
