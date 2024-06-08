using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Utility;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Entities;

[Table("Team")]
public partial class Team : EntityBase
{
    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? ImagePath { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string ColorHex { get; set; } = null!;

    [InverseProperty("Team")]
    public virtual ICollection<SessionResult> SessionResults { get; set; } = new List<SessionResult>();

    [InverseProperty("Team")]
    public virtual ICollection<SeasonDrivers> SeasonDrivers { get; set; } = new List<SeasonDrivers>();
}
