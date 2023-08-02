using F1StatsServer.Dto.DriverDtos;
using F1StatsServer.Dto.ResultsDtos;
using F1StatsServer.Dto.TrackDtos;
using F1StatsServer.Model;

namespace F1StatsServer.Dto.GrandPrixDtos
{
    public class GrandPrixDisplayDto
    {
        public string? GrandPrixName { get; set; }

        public DateTimeOffset? GrandPrixDate { get; set; }

        public string? YoutubeUrl { get; set; }

        public TrackDto? Track { get; set; }

        public IEnumerable<RaceDto>? Race { get; set; }

        public IEnumerable<QualDto>? Qualifying { get; set; }

        public IEnumerable<RaceSprintDto>? Sprint { get; set; }

        public IEnumerable<TeamDto>? Teams { get; set; }
        
        public IEnumerable<DriverGrandPrixDto>? Drivers { get; set; }
    }
}
