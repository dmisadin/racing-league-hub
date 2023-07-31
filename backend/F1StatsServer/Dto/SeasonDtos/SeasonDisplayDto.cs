using F1StatsServer.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using F1StatsServer.Dto.GrandPrixDto;
using F1StatsServer.Dto.DriverDto;

namespace F1StatsServer.Dto.SeasonDtos
{
    public class SeasonDisplayDto
    {
        [StringLength(255)]
        public string Name { get; set; } = null!;

        [StringLength(255)]
        [Unicode(false)]
        public string? ImagePath { get; set; }

        public byte LapsRequiredPercentage { get; set; }

        public GameDto? Game { get; set; }

        public PlatformDto? Platform { get; set; }

        public List<SeasonPointsDto>? RacePoints { get; set; }

        public List<SeasonPointsDto>? QualPoints { get; set; }

        public List<SeasonPointsDto>? SprintPoints { get; set; }

        public SeasonPointsDto? FastestLapPoints { get; set; }

        public SeasonLobbySettingsDto? LobbySettings { get; set; }

        public SeasonAssistsDto? Assists { get; set; }

        public List<GrandPrixSeasonDto>? GrandPrixes { get; set; }

        public List<DriverSeasonDto>? Drivers { get; set; } 

    }
}
