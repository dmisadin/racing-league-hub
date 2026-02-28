namespace RacingLeagueHub.Dto.ResultsDtos
{
    public class SprintResultDto : SessionResultBaseDto
    {
        public decimal? RaceTime { get; set; }
        public int? TimePenalty { get; set; }
        public int? LapsCompleted { get; set; }
        public int? GridPosition { get; set; }
        public string? UsedTyres { get; set; }
    }
}
