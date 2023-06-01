using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Model;

[Keyless]
[Table("GrandPrixTrack")]
public partial class GrandPrixTrack
{
    [Column("FK_GrandPrixTrack_GrandPrixId")]
    public int FkGrandPrixTrackGrandPrixId { get; set; }

    [Column("FK_GrandPrixTrack_TrackId")]
    public short FkGrandPrixTrackTrackId { get; set; }

    [ForeignKey("FkGrandPrixTrackGrandPrixId")]
    public virtual GrandPrix FkGrandPrixTrackGrandPrix { get; set; } = null!;

    [ForeignKey("FkGrandPrixTrackTrackId")]
    public virtual Track FkGrandPrixTrackTrack { get; set; } = null!;
}
