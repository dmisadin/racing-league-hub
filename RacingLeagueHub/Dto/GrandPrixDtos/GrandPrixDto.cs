using RacingLeagueHub.Dto.DriverDtos;
using RacingLeagueHub.Dto.ResultsDtos;
using RacingLeagueHub.Dto.TrackDtos;
using RacingLeagueHub.Entities;

namespace RacingLeagueHub.Dto.GrandPrixDtos
{
    public class GrandPrixDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public string? YoutubeUrl { get; set; }
        public int? FastestDriverId { get; set; }

        public TrackDto? Track { get; set; }
        public IEnumerable<RaceResultDto>? Race { get; set; }
        public IEnumerable<QualifyingResultDto>? Qualifying { get; set; }
        public IEnumerable<SprintResultDto>? Sprint { get; set; }
        public IEnumerable<TeamDto>? Teams { get; set; }
        public IEnumerable<DriverGrandPrixDto>? Drivers { get; set; }
    }
}
