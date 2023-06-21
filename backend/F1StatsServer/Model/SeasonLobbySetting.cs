using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Keyless]
public partial class SeasonLobbySetting
{
    [Column("FK_SeasonLobbySettings_SeasonId")]
    public int FkSeasonLobbySettingsSeasonId { get; set; }

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

    [ForeignKey("FkSeasonLobbySettingsSeasonId")]
    public virtual Season FkSeasonLobbySettingsSeason { get; set; } = null!;
}
