using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Table("GrandPrix")]
public partial class GrandPrix
{
    [Key]
    [Column("PK_GrandPrixId")]
    public int PkGrandPrixId { get; set; }

    [Column("FK_GrandPrix_SeasonId")]
    public int FkGrandPrixSeasonId { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    public DateTimeOffset StartTime { get; set; }

    public bool HasSprint { get; set; }

    [Column("YouTubeURL")]
    [StringLength(255)]
    [Unicode(false)]
    public string? YouTubeUrl { get; set; }

    [ForeignKey("FkGrandPrixSeasonId")]
    [InverseProperty("GrandPrixes")]
    public virtual Season FkGrandPrixSeason { get; set; } = null!;

    [InverseProperty("FkQualifyingGrandPrix")]
    public virtual ICollection<Qualifying> Qualifyings { get; set; } = new List<Qualifying>();

    [InverseProperty("FkRaceGrandPrix")]
    public virtual ICollection<Race> Races { get; set; } = new List<Race>();

    [InverseProperty("FkSprintDriverGrandPrix")]
    public virtual ICollection<Sprint> Sprints { get; set; } = new List<Sprint>();

    [ForeignKey("FkGrandPrixCountryGrandPrixId")]
    [InverseProperty("FkGrandPrixCountryGrandPrixes")]
    public virtual ICollection<Country> FkGrandPrixCountryCountries { get; set; } = new List<Country>();

    [ForeignKey("FkGrandPrixTrackGrandPrixId")]
    [InverseProperty("FkGrandPrixTrackGrandPrixes")]
    public virtual ICollection<Track> FkGrandPrixTrackTracks { get; set; } = new List<Track>();
}
