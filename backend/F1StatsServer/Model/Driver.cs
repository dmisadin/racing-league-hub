using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Table("Driver")]
public partial class Driver : EntityBase
{

    public int? PlatformId { get; set; }

    public int? SocialMediaId { get; set; }

    [StringLength(40)]
    public string Name { get; set; } = null!;

    [StringLength(64)]
    [Unicode(false)]
    public string? Wheel { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? Pedals { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? Controller { get; set; }

    [ForeignKey("PlatformId")]
    [InverseProperty("Drivers")]
    public virtual Platform? Platform { get; set; }

    [InverseProperty("Driver")]
    public virtual ICollection<Qualifying> Qualifyings { get; set; } = new List<Qualifying>();

    [InverseProperty("Driver")]
    public virtual ICollection<Race> Races { get; set; } = new List<Race>();

    [InverseProperty("Driver")]
    public virtual ICollection<SeasonDriver> SeasonDrivers { get; set; } = new List<SeasonDriver>();

    [ForeignKey("SocialMediaId")]
    [InverseProperty("Drivers")]
    public virtual SocialMedium? SocialMedia { get; set; }

    [InverseProperty("Driver")]
    public virtual ICollection<Sprint> Sprints { get; set; } = new List<Sprint>();

    [ForeignKey("DriverId")]
    [InverseProperty("Drivers")]
    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();
}
