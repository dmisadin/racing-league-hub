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

        public List<SeasonPointsDto> RacePointsDto { get; set; } = null!;
        public SeasonLobbySettingsDto LobbySettingsDto { get; set; } = null!;
        public SeasonAssistsDto AssistsDto { get; set; } = null!;
        public List<SeasonPointsDto>? QualPointsDto { get; set; }
        public List<SeasonPointsDto>? SprintPointsDto { get; set; }
        public SeasonPointsDto FastestLapPointDto { get; set; } = null!;

    }
}
