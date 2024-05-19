using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Utility;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Entities;

public partial class SocialMedia : EntityBase
{
    [StringLength(255)]
    [Unicode(false)]
    public string? Website { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Discord { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Youtube { get; set; }

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
