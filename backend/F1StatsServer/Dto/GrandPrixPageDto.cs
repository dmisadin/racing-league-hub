using F1StatsServer.Model;

namespace F1StatsServer.Dto
{
    public class GrandPrixPageDto
    {
        public string? GrandPrixName { get; set; }
        public DateTimeOffset? GrandPrixDate { get; set; }
        public string? YoutubeUrl { get; set; }
        public TrackDto? Track { get; set; }
        public IEnumerable<RaceDto>? Race{ get; set;}
    }
}
