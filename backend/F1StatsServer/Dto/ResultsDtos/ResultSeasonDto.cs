namespace F1StatsServer.Dto.ResultsDtos
{
    public class ResultSeasonDto
    {
        public int? DriverId { get; set; }

        public int? TeamId { get; set; }

        public byte? PointsGained { get; set; }

        public byte? ResultStatus { get; set; }

    }
}
