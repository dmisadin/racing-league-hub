using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Keyless]
[Table("GrandPrixCountry")]
public partial class GrandPrixCountry
{
    [Column("FK_GrandPrixCountry_GrandPrixId")]
    public int FkGrandPrixCountryGrandPrixId { get; set; }

    [Column("FK_GrandPrixCountry_CountryId")]
    public short FkGrandPrixCountryCountryId { get; set; }

    [ForeignKey("FkGrandPrixCountryCountryId")]
    public virtual Country FkGrandPrixCountryCountry { get; set; } = null!;

    [ForeignKey("FkGrandPrixCountryGrandPrixId")]
    public virtual GrandPrix FkGrandPrixCountryGrandPrix { get; set; } = null!;
}
