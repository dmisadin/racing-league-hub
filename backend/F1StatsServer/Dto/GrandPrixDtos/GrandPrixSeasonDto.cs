using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using F1StatsServer.Dto.ResultsDtos;
using F1StatsServer.Dto.TrackDtos;

namespace F1StatsServer.Dto.GrandPrixDtos
{
    public class GrandPrixSeasonDto
    {
        [Required, NotNull]
        public string? Name { get; set; }

        public DateTimeOffset? StartTime { get; set; }

        [DefaultValue(false)]
        public bool HasSprint { get; set; }

        [DefaultValue("https://www.youtube.com")]
        public string? YoutubeUrl { get; set; }

        public int? FastestDriverId { get; set; }

        public TrackSeasonDto? Track { get; set; }

        public List<ResultSeasonDto>? Race { get; set; }

        public List<ResultSeasonDto>? Qualifying { get; set; }

        public List<ResultSeasonDto>? Sprint { get; set; }
    }
}
