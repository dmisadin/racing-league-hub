using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace F1StatsServer.Dto
{
    public class LeagueDto
    {
        [Required, NotNull]
        public int RegionId { get; set; }
        [Required, NotNull]
        public string? Name { get; set; }
        [Required, NotNull, DefaultValue("#000000")]
        public string? ColorHex { get; set; }

    }
}
