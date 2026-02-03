using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Utility;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Entities;

[Table("Country")]
public partial class Country : EntityBase
{
    [StringLength(64)]
    [Unicode(false)]
    public string NameEnglish { get; set; } = null!;

    [StringLength(64)]
    public string NameCroatian { get; set; } = null!;

    [Column("ISO")]
    [StringLength(2)]
    [Unicode(false)]
    public string Iso { get; set; } = null!;

    [Column("ISO3")]
    [StringLength(3)]
    [Unicode(false)]
    public string Iso3 { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? ImagePath { get; set; }

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
