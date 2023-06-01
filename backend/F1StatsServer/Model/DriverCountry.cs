using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Keyless]
[Table("DriverCountry")]
public partial class DriverCountry
{
    [Column("FK_DriverCountry_DriverId")]
    public int FkDriverCountryDriverId { get; set; }

    [Column("FK_DriverCountry_CountryId")]
    public short FkDriverCountryCountryId { get; set; }

    [ForeignKey("FkDriverCountryCountryId")]
    public virtual Country FkDriverCountryCountry { get; set; } = null!;

    [ForeignKey("FkDriverCountryDriverId")]
    public virtual Driver FkDriverCountryDriver { get; set; } = null!;
}
