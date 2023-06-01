using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Table("Track")]
public partial class Track
{
    [Key]
    [Column("PK_TrackId")]
    public short PkTrackId { get; set; }

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
}
