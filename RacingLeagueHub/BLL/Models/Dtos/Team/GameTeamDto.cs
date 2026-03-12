using RacingLeagueHub.BLL.Models.Enums;

namespace RacingLeagueHub.BLL.Models.Dtos.Team
{
    public class GameTeamDto : BaseDto
    {
        public Game Game { get; set; }
        public long TeamId { get; set; }
        public string? DisplayName { get; set; }
        public string? Color { get; set; }
        public short TelemetryId { get; set; }
    }
}
