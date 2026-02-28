using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Dto.ResultsDtos;
using RacingLeagueHub.Dto.TrackDtos;

namespace RacingLeagueHub.Dto.GrandPrixDtos
{
    public class GrandPrixSeasonDto
    {
        public int Id { get; set; }

        [Required, NotNull]
        public string? Name { get; set; }

        public DateTimeOffset? StartTime { get; set; }

        [DefaultValue(false)]
        public bool HasSprint { get; set; }

        [DefaultValue("https://www.youtube.com")]
        public string? YoutubeUrl { get; set; }

        public int? FastestDriverId { get; set; }

        public TrackSeasonDto? Track { get; set; }

        public List<SessionResultBaseDto>? Race { get; set; }

        public List<SessionResultBaseDto>? Qualifying { get; set; }

        public List<SessionResultBaseDto>? Sprint { get; set; }
    }
}
