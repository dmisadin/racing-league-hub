using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Entities.Enums;
using F1StatsServer.Utility;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Entities;

[Table("Season")]
public partial class Season : EntityBase
{
    public int LeagueId { get; set; }
    [StringLength(255)]
    public string Name { get; set; } = null!;
    public virtual Game Game { get; set; }
    public virtual Platform? Platform { get; set; }
    [StringLength(255)]
    [Unicode(false)]
    public string? ImagePath { get; set; }
    public byte LapsRequiredPercentage { get; set; }

    [InverseProperty("Season")]
    public virtual ICollection<GrandPrix> GrandPrixes { get; set; } = new List<GrandPrix>();

    [ForeignKey("LeagueId")]
    [InverseProperty("Seasons")]
    public virtual League League { get; set; } = null!;


    [InverseProperty("Season")]
    public virtual SeasonAssists SeasonAssist { get; set; }

    [InverseProperty("Season")]
    public virtual ICollection<SeasonDrivers> SeasonDrivers { get; set; } = new List<SeasonDrivers>();

    [InverseProperty("Season")]
    public virtual SeasonLobbySettings SeasonLobbySetting { get; set; }

    [InverseProperty("Season")]
    public virtual ICollection<SeasonPoints> SeasonPoints { get; set; } = new List<SeasonPoints>();
}
