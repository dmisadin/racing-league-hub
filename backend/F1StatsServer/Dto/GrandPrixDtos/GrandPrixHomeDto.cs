using F1StatsServer.Dto.LeagueDtos;
using F1StatsServer.Dto.SeasonDtos;
using Microsoft.OpenApi.Any;

namespace F1StatsServer.Dto.GrandPrixDtos
{
    public class GrandPrixHomeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public SeasonHomeDto? Season { get; set; }
        public LeagueHomeDto? League { get; set; }

    }
}
