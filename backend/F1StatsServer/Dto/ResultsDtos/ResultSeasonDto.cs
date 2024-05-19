using F1StatsServer.Model.Enums;

namespace F1StatsServer.Dto.ResultsDtos
{
    public class ResultSeasonDto
    {
        public int? DriverId { get; set; }

        public int? TeamId { get; set; }

        public ResultStatus ResultStatus { get; set; }

        public byte? PointsGained { get; set; }

    }
}
