using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using F1StatsServer.Dto.SeasonDtos;
using F1StatsServer.Model.Enums;

namespace F1StatsServer.Dto.LeagueDtos
{
    public class LeagueDisplayDto
    {

        [Required, NotNull]
        public string? Name { get; set; }

        [Required, NotNull, DefaultValue("")]
        public string? Description { get; set; }

        [Required, NotNull, DefaultValue("#000000")]
        public string? ColorHex { get; set; }

        [DefaultValue("/")]
        public string? ImagePath { get; set; }

        public SocialMediaDto? SocialMedia { get; set; }

        public Region? Region { get; set; }

        public List<SeasonsInLeagueDto>? SeasonsInLeague { get; set; }
    }
}
