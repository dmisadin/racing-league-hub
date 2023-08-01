using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Table("Season")]
public partial class Season : EntityBase
{
    public int LeagueId { get; set; }

    public int GameId { get; set; }

    public int? PlatformId { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? ImagePath { get; set; }

    public byte LapsRequiredPercentage { get; set; }

    [ForeignKey("GameId")]
    [InverseProperty("Seasons")]
    public virtual Game Game { get; set; } = null!;

    [InverseProperty("Season")]
    public virtual ICollection<GrandPrix> GrandPrixes { get; set; } = new List<GrandPrix>();

    [ForeignKey("LeagueId")]
    [InverseProperty("Seasons")]
    public virtual League League { get; set; } = null!;

    [ForeignKey("PlatformId")]
    [InverseProperty("Seasons")]
    public virtual Platform? Platform { get; set; }

    [InverseProperty("Season")]
    public virtual SeasonAssists SeasonAssist { get; set; }

    [InverseProperty("Season")]
    public virtual ICollection<SeasonDrivers> SeasonDrivers { get; set; } = new List<SeasonDrivers>();

    [InverseProperty("Season")]
    public virtual SeasonFastestLapPoints SeasonFastestLapPoint { get; set; }

    [InverseProperty("Season")]
    public virtual SeasonLobbySettings SeasonLobbySetting { get; set; }

    [InverseProperty("Season")]
    public virtual ICollection<SeasonQualPoints> SeasonQualPoints { get; set; } = new List<SeasonQualPoints>();

    [InverseProperty("Season")]
    public virtual ICollection<SeasonRacePoints> SeasonRacePoints { get; set; } = new List<SeasonRacePoints>();

    [InverseProperty("Season")]
    public virtual ICollection<SeasonSprintPoints> SeasonSprintPoints { get; set; } = new List<SeasonSprintPoints>();
}
