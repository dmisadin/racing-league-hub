using System.ComponentModel;

namespace F1StatsServer.Dto.LeagueDtos
{
    public class LeaguesDisplayDto
    {
        public int? Id { get; set; }
        
        public string? Name { get; set; }

        public string? ColorHex { get; set; }

        public string? ImagePath { get; set; }

        public PlatformDto? Platform { get; set; }

        public GameDto? Game { get; set; }
    }
}
