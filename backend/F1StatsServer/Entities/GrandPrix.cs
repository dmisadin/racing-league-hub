using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Utility;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Entities;

[Table("GrandPrix")]
public partial class GrandPrix : EntityBase
{
    public int SeasonId { get; set; }

    public int? TrackId { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    public DateTimeOffset StartTime { get; set; }

    public bool HasSprint { get; set; }

    [Column("YoutubeUrl")]
    [StringLength(255)]
    [Unicode(false)]
    public string? YoutubeUrl { get; set; }

    [InverseProperty("GrandPrix")]
    public virtual ICollection<SessionResult> SessionResults { get; set; } = new List<SessionResult>();

    [ForeignKey("SeasonId")]
    [InverseProperty("GrandPrixes")]
    public virtual Season Season { get; set; } = null!;

    [ForeignKey("TrackId")]
    [InverseProperty("GrandPrixes")]
    public virtual Track? Track { get; set; }

}
