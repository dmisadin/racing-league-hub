using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace F1StatsServer.Dto.SeasonDtos
{
    public class SeasonInsertDto
    {
        public int LeagueId { get; set; }

        public int GameId { get; set; }

        public int? PlatformId { get; set; }

        [StringLength(255)]
        public string Name { get; set; } = null!;

        [StringLength(255)]
        [Unicode(false)]
        public string? ImagePath { get; set; }

        public byte LapsRequiredPercentage { get; set; }

        public List<SeasonPointDto> RacePointsDto { get; set; } = null!;
        public SeasonLobbySettingDto LobbySettingDto { get; set; } = null!;
        public SeasonAssistDto AssistDto { get; set; } = null!;
        public List<SeasonPointDto>? QualPointsDto { get; set; }
        public List<SeasonPointDto>? SprintPointsDto { get; set; }
        public SeasonPointDto FastestLapPointDto { get; set; } = null!;

    }
}
