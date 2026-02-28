using RacingLeagueHub.Dto.LeagueDtos;
using RacingLeagueHub.Dto.SeasonDtos;
using RacingLeagueHub.Dto.TrackDtos;

namespace RacingLeagueHub.Dto.GrandPrixDtos
{
    public class GrandPrixHomeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public string? YoutubeUrl { get; set; }

        public SeasonHomeDto? Season { get; set; }
        public LeagueHomeDto? League { get; set; }
        public TrackHomeDto? Track { get; set; }
        

    }
}
