using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Dto.GrandPrixDto
{
    public class GrandPrixSeasonDto
    {
        [Required, NotNull]
        public string? Name { get; set; }

        //public DateTimeOffset Date { get; set; } //Will be added in the future

        [DefaultValue(false)]
        public bool HasSprint { get; set; }

        [DefaultValue("https://www.youtube.com")]
        public string? YoutubeUrl { get; set; }

        public byte? Laps { get; set; }


        [StringLength(2)]
        [Unicode(false)]
        public string? CountryIso { get; set; }

        public int? FastestDriverId { get; set; }

        public List<ResultSeasonDto>? Races { get; set; }

        public List<ResultSeasonDto>? Qualifications { get; set; }

        public List<ResultSeasonDto>? Sprints { get; set; }
    }
}
