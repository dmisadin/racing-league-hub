using F1StatsServer.Model.Enums;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1StatsServer.Model;

[Table("Driver")]
public partial class Driver : EntityBase
{
    public int? PlatformId { get; set; }

    public int? SocialMediaId { get; set; }

    public int? CountryId { get; set; }

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

    public virtual Platform? Platform { get; set; }

    [InverseProperty("Driver")]
    public virtual ICollection<Qualifying> Qualifyings { get; set; } = new List<Qualifying>();

    [InverseProperty("Driver")]
    public virtual ICollection<Race> Races { get; set; } = new List<Race>();

    [InverseProperty("Driver")]
    public virtual ICollection<SeasonDrivers> SeasonDrivers { get; set; } = new List<SeasonDrivers>();

    [ForeignKey("SocialMediaId")]
    [InverseProperty("Drivers")]
    public virtual SocialMedia? SocialMedia { get; set; }

    [InverseProperty("Driver")]
    public virtual ICollection<Sprint> Sprints { get; set; } = new List<Sprint>();

    [ForeignKey("CountryId")]
    [InverseProperty("Drivers")]
    public virtual Country? Country { get; set; }
}
