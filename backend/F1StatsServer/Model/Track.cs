using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Table("Track")]
public partial class Track : EntityBase
{
    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public byte? CornersTotal { get; set; }

    public byte? CornersLeft { get; set; }

    [Column(TypeName = "decimal(5, 1)")]
    public decimal? Elevation { get; set; }

    public short? Length { get; set; }

    public byte? PitStop { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? ImagePath { get; set; }

    public byte Laps { get; set; }

    public virtual ICollection<GrandPrix> GrandPrixes { get; set; } = new List<GrandPrix>();

    [ForeignKey("TrackId")]
    [InverseProperty("Tracks")]
    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();

}
