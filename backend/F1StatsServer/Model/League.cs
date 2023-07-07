using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;

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

    [ForeignKey("RegionId")]
    [InverseProperty("Leagues")]
    public virtual Region Region { get; set; } = null!;

    [InverseProperty("League")]
    public virtual ICollection<Season> Seasons { get; set; } = new List<Season>();

    [ForeignKey("SocialMediaId")]
    [InverseProperty("Leagues")]
    public virtual SocialMedium? SocialMedia { get; set; }

    [ForeignKey("LeagueId")]
    [InverseProperty("Leagues")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
