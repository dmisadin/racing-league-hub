using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Table("Driver")]
public partial class Driver
{
    [Key]
    [Column("PK_DriverId")]
    public int PkDriverId { get; set; }

    [StringLength(40)]
    public string Name { get; set; } = null!;
}
