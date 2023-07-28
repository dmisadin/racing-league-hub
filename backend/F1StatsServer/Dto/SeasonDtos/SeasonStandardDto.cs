using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace F1StatsServer.Dto.SeasonDtos
{
    public class SeasonStandardDto
    {
        [Required, NotNull]
        public int LeagueId { get; set; }

        [Required, NotNull]
        public int GameId { get; set; }

        [Required, NotNull]
        public string Name { get; set; } = null!;

        [Required, NotNull]
        public string Game { get; set; } = null!;

    }
}
