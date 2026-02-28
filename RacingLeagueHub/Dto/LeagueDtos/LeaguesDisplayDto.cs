using RacingLeagueHub.Entities.Enums;
using System.ComponentModel;

namespace RacingLeagueHub.Dto.LeagueDtos
{
    public class LeaguesDisplayDto
    {
        public int? Id { get; set; }
        
        public string? Name { get; set; }

        public string? ColorHex { get; set; }

        public string? ImagePath { get; set; }

        public Platform? Platform { get; set; }

        public Game Game { get; set; }
    }
}
