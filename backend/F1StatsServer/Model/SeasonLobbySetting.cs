using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

public partial class SeasonLobbySetting : EntityBase
{
    public int SeasonId { get; set; }

    [StringLength(8)]
    [Unicode(false)]
    public string Qualifying { get; set; } = null!;

    public byte RaceDistancePercentage { get; set; }

    public bool FormationLap { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Weather { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string CornerCutting { get; set; } = null!;

    [StringLength(12)]
    [Unicode(false)]
    public string CarDamage { get; set; } = null!;

    [StringLength(12)]
    [Unicode(false)]
    public string CarDamageRate { get; set; } = null!;

    public bool ParcFerme { get; set; }

    public bool EqualCarPerformance { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string SafetyCar { get; set; } = null!;

    public bool Collisions { get; set; }

    public bool Ghosting { get; set; }

    [ForeignKey("SeasonId")]
    [InverseProperty("SeasonLobbySettings")]
    public virtual Season Season { get; set; } = null!;
}
