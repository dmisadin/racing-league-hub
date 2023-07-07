using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Table("TrackCountry")]
public partial class TrackCountry : EntityBase
{
    public int TrackId { get; set; }

    public int CountryId { get; set; }

    public bool IsOwner { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("TrackCountries")]
    public virtual Country Country { get; set; } = null!;

    [ForeignKey("TrackId")]
    [InverseProperty("TrackCountries")]
    public virtual Track Track { get; set; } = null!;
}
