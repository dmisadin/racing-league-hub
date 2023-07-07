using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

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
    public virtual ICollection<Qualifying> Qualifyings { get; set; } = new List<Qualifying>();

    [InverseProperty("Team")]
    public virtual ICollection<Race> Races { get; set; } = new List<Race>();

    [InverseProperty("Team")]
    public virtual ICollection<SeasonDriver> SeasonDrivers { get; set; } = new List<SeasonDriver>();

    [InverseProperty("Team")]
    public virtual ICollection<Sprint> Sprints { get; set; } = new List<Sprint>();
}
