using RacingLeagueHub.Entities.Enums;

namespace RacingLeagueHub.Dto.SeasonDtos
{
    public class SeasonPointsDto
    {
        public byte Position { get; set; }
        public byte Points { get; set; }
        public PointsType PointsType { get; set; }
    }
}
