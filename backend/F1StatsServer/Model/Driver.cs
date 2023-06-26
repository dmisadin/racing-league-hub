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

    [InverseProperty("FkQualifyingDriver")]
    public virtual ICollection<Qualifying> Qualifyings { get; set; } = new List<Qualifying>();

    [InverseProperty("FkRaceDriver")]
    public virtual ICollection<Race> Races { get; set; } = new List<Race>();

    [InverseProperty("FkSprintDriverDriver")]
    public virtual ICollection<Sprint> Sprints { get; set; } = new List<Sprint>();

    [ForeignKey("FkDriverCountryDriverId")]
    [InverseProperty("FkDriverCountryDrivers")]
    public virtual ICollection<Country> FkDriverCountryCountries { get; set; } = new List<Country>();
}
