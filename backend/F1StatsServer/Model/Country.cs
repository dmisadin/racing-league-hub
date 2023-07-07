using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

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

    [InverseProperty("Country")]
    public virtual ICollection<TrackCountry> TrackCountries { get; set; } = new List<TrackCountry>();

    [ForeignKey("CountryId")]
    [InverseProperty("Countries")]
    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    [ForeignKey("CountryId")]
    [InverseProperty("Countries")]
    public virtual ICollection<GrandPrix> GrandPrixes { get; set; } = new List<GrandPrix>();
}
