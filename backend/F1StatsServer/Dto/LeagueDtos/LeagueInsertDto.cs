using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Dto.LeagueDto
{
    public class LeagueInsertDto
    {
        [Required, NotNull]
        public int RegionId { get; set; }

        [Required, NotNull]
        public string? Name { get; set; }

        [DefaultValue("")]
        public string? Description { get; set; }

        [Required, NotNull, DefaultValue("#000000")]
        public string? ColorHex { get; set; }

        [DefaultValue("/")]
        public string? ImagePath { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        [DefaultValue("/")]
        public string? Website { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        [DefaultValue("/")]
        public string? Discord { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        [DefaultValue("/")]
        public string? YouTube { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        [DefaultValue("/")]
        public string? Twitch { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        [DefaultValue("/")]
        public string? Facebook { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        [DefaultValue("/")]
        public string? Instagram { get; set; }

        //[StringLength(255)]
        //[Unicode(false)]
        //public string? Twitter { get; set; }
    }
}
