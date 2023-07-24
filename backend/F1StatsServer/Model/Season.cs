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
    public virtual Game GameNavigation { get; set; } = null!;

    [InverseProperty("Season")]
    public virtual ICollection<GrandPrix> GrandPrixes { get; set; } = new List<GrandPrix>();

    [ForeignKey("LeagueId")]
    [InverseProperty("Seasons")]
    public virtual League League { get; set; } = null!;

    [ForeignKey("PlatformId")]
    [InverseProperty("Seasons")]
    public virtual Platform? Platform { get; set; }

    [InverseProperty("Season")]
    public virtual SeasonAssist SeasonAssist { get; set; }

    [InverseProperty("Season")]
    public virtual ICollection<SeasonDriver> SeasonDrivers { get; set; } = new List<SeasonDriver>();

    [InverseProperty("Season")]
    public virtual SeasonFastestLapPoint SeasonFastestLapPoint { get; set; }

    [InverseProperty("Season")]
    public virtual SeasonLobbySetting SeasonLobbySetting { get; set; }

    [InverseProperty("Season")]
    public virtual ICollection<SeasonQualPoint> SeasonQualPoints { get; set; } = new List<SeasonQualPoint>();

    [InverseProperty("Season")]
    public virtual ICollection<SeasonRacePoint> SeasonRacePoints { get; set; } = new List<SeasonRacePoint>();

    [InverseProperty("Season")]
    public virtual ICollection<SeasonSprintPoint> SeasonSprintPoints { get; set; } = new List<SeasonSprintPoint>();
}
