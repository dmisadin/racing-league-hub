using F1StatsServer.Entities.Enums;

namespace F1StatsServer.Dto.SeasonDtos
{
    public class SeasonPointsDto
    {
        public byte Position { get; set; }
        public byte Points { get; set; }
        public PointsType PointsType { get; set; }
    }
}
