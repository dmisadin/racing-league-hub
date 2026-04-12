using RacingLeagueHub.Application.Dtos.Resource;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Models.Enums;

namespace RacingLeagueHub.Application.Dtos.Team
{
    public class GameTeamDto : BaseDto
    {
        public Game Game { get; set; }
        public EncryptedId TeamId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Abbreviation { get; set; }
        public string? Color { get; set; }
        public short TelemetryId { get; set; }
        public EncryptedId? LogoResourceId { get; set; }
        public string? LogoUrl { get; set; }
    }
}
