using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Table("Team")]
public partial class Team
{
    [Key]
    [Column("PK_TeamId")]
    public byte PkTeamId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? ImagePath { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string ColorHex { get; set; } = null!;

    [InverseProperty("FkQualifyingTeam")]
    public virtual ICollection<Qualifying> Qualifyings { get; set; } = new List<Qualifying>();

    [InverseProperty("FkRaceTeam")]
    public virtual ICollection<Race> Races { get; set; } = new List<Race>();

    [InverseProperty("FkSprintTeam")]
    public virtual ICollection<Sprint> Sprints { get; set; } = new List<Sprint>();
}
