using RacingLeagueHub.API.Dtos.Resource;
using RacingLeagueHub.BLL.Models.Enums;

namespace RacingLeagueHub.BLL.Models.Dtos.Team
{
    public class GameTeamDto : BaseDto
    {
        public Game Game { get; set; }
        public long TeamId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Abbreviation { get; set; }
        public string? Color { get; set; }
        public short TelemetryId { get; set; }
        public long? LogoResourceId { get; set; }
        public ResourceBaseDto? Logo { get; set; }
    }
}
