namespace RacingLeagueHub.Dto.SeasonDtos
{
    public class SeasonSessionPointsDto
    {
        public List<SeasonPointsDto>? RacePoints { get; set; }

        public List<SeasonPointsDto>? QualPoints { get; set; }

        public List<SeasonPointsDto>? SprintPoints { get; set; }

        public SeasonPointsDto? FastestLapPoints { get; set; }
    }
}
