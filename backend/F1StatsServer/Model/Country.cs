using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Table("Country")]
public partial class Country
{
    [Key]
    [Column("PK_CountryId")]
    public short PkCountryId { get; set; }

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
}
