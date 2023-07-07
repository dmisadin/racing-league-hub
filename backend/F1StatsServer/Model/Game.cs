using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using F1StatsServer.Util;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Table("Game")]
public partial class Game : EntityBase
{

    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("GameNavigation")]
    public virtual ICollection<Season> Seasons { get; set; } = new List<Season>();
}
