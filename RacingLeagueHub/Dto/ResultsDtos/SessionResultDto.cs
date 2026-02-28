using F1StatsServer.Entities.Enums;

namespace F1StatsServer.Dto.ResultsDtos
{
    public class SessionResultDto : SessionResultBaseDto
    {
        public SessionType SessionType { get; set; }
        public int GrandPrixId { get; set; }
        public decimal? RaceTime { get; set; }
        public int? FastestLapInMS { get; set; }
        public int? TimePenalty { get; set; }
        public int? PostRaceTimePenalty { get; set; }
        public int? LapsCompleted { get; set; }
        public int? GridPosition { get; set; }
        public string? UsedTyres { get; set; }
        public string? BestTimeTyre { get; set; }
    }
}
