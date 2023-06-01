using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Keyless]
[Table("TrackCountry")]
public partial class TrackCountry
{
    [Column("FK_TrackCountry_TrackId")]
    public short FkTrackCountryTrackId { get; set; }

    [Column("FK_TrackCountry_CountryId")]
    public short FkTrackCountryCountryId { get; set; }

    [ForeignKey("FkTrackCountryCountryId")]
    public virtual Country FkTrackCountryCountry { get; set; } = null!;

    [ForeignKey("FkTrackCountryTrackId")]
    public virtual Track FkTrackCountryTrack { get; set; } = null!;
}
