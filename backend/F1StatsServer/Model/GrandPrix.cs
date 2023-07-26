using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Table("GrandPrix")]
public partial class GrandPrix : EntityBase
{
    public int SeasonId { get; set; }

    public int? TrackId { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    public DateTimeOffset StartTime { get; set; }

    public bool HasSprint { get; set; }

    [Column("YouTubeURL")]
    [StringLength(255)]
    [Unicode(false)]
    public string? YouTubeUrl { get; set; }

    [InverseProperty("GrandPrix")]
    public virtual ICollection<Qualifying> Qualifyings { get; set; } = new List<Qualifying>();

    [InverseProperty("GrandPrix")]
    public virtual ICollection<Race> Races { get; set; } = new List<Race>();

    [ForeignKey("SeasonId")]
    [InverseProperty("GrandPrixes")]
    public virtual Season Season { get; set; } = null!;

    [ForeignKey("TrackId")]
    [InverseProperty("GrandPrixes")]
    public virtual Track? Track { get; set; }

    [InverseProperty("GrandPrix")]
    public virtual ICollection<Sprint> Sprints { get; set; } = new List<Sprint>();

}
