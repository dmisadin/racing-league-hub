using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace F1StatsServer.Dto.LeagueDto
{
    public class LeagueStandardDto
    {
        public int Id { get; set; }
        [Required, NotNull]
        public int RegionId { get; set; }

        public int SocialMediaId { get; set; }

        [Required, NotNull]
        public string? Name { get; set; }

        [Required, NotNull, DefaultValue("")]
        public string? Description { get; set; }

        [Required, NotNull, DefaultValue("#000000")]
        public string? ColorHex { get; set; }

        [DefaultValue("/")]
        public string? ImagePath { get; set; }

    }
}
