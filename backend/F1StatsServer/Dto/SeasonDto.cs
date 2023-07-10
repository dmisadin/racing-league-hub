using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace F1StatsServer.Dto
{
    public class SeasonDto
    {
        public int LeagueId { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; } = null!;
        public string Game { get; set; } = null!;

    }
}
