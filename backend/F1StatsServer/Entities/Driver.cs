using F1StatsServer.Entities.Enums;
using F1StatsServer.Utility;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1StatsServer.Entities;

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
    public virtual ICollection<SessionResult> SessionResults { get; set; } = new List<SessionResult>();

    [InverseProperty("Driver")]
    public virtual ICollection<SeasonDrivers> SeasonDrivers { get; set; } = new List<SeasonDrivers>();

    [ForeignKey("SocialMediaId")]
    [InverseProperty("Drivers")]
    public virtual SocialMedia? SocialMedia { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("Drivers")]
    public virtual Country? Country { get; set; }
}
