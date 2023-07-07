using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

public partial class SocialMedium : EntityBase
{

    [StringLength(255)]
    [Unicode(false)]
    public string? Website { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Discord { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? YouTube { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Twitch { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Facebook { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Instagram { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Twitter { get; set; }

    [InverseProperty("SocialMedia")]
    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    [InverseProperty("SocialMedia")]
    public virtual ICollection<League> Leagues { get; set; } = new List<League>();
}
