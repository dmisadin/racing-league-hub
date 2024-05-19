using F1StatsServer.Model.Enums;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1StatsServer.Model;

[Table("League")]
public partial class League : EntityBase
{
    public int RegionId { get; set; }

    public int? SocialMediaId { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? ImagePath { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Description { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string ColorHex { get; set; } = null!;
    public virtual Region Region { get; set; }

    [InverseProperty("League")]
    public virtual ICollection<LeagueUser> LeagueUsers { get; set; } = new List<LeagueUser>();


    [InverseProperty("League")]
    public virtual ICollection<Season> Seasons { get; set; } = new List<Season>();

    [ForeignKey("SocialMediaId")]
    [InverseProperty("Leagues")]
    public virtual SocialMedia? SocialMedia { get; set; }
}
