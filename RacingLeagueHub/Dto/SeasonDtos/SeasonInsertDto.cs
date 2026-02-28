using F1StatsServer.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace F1StatsServer.Dto.SeasonDtos
{
    public class SeasonInsertDto
    {
        public int LeagueId { get; set; }

        public Game Game { get; set; }

        public Platform Platform { get; set; }

        [StringLength(255)]
        public string Name { get; set; } = null!;

        [StringLength(255)]
        [Unicode(false)]
        public string? ImagePath { get; set; }

        public byte LapsRequiredPercentage { get; set; }

        public IEnumerable<SeasonPointsDto> SeasonPoints { get; set; } = null!;
        public SeasonLobbySettingsDto LobbySettings { get; set; } = null!;
        public SeasonAssistsDto Assists { get; set; } = null!;

    }
}
