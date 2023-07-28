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

        public List<SeasonPointDto>? RacePoints { get; set; }

        public List<SeasonPointDto>? QualPoints { get; set; }

        public List<SeasonPointDto>? SprintPoints { get; set; }

        public SeasonPointDto? FastestLapPoint { get; set; }

        public SeasonLobbySettingDto? LobbySetting { get; set; }

        public SeasonAssistDto? Assist { get; set; }

        public List<GrandPrixSeasonDto>? GrandPrixes { get; set; }

        public List<DriverSeasonDto>? Drivers { get; set; } 

    }
}
