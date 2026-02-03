using F1StatsServer.Entities.Enums;

namespace F1StatsServer.Dto.ResultsDtos
{
    public class SessionResultBaseDto
    {
        public int? Id { get; set; }
        public int GrandPrixId { get; set; }
        public int? DriverId { get; set; }
        public int? TeamId { get; set; }
        public int Position { get; set; }
        public int? FastestLapInMs { get; set; }
        public ResultStatus ResultStatus { get; set; }
        public int? PointsGained { get; set; }
        public bool IsReserve { get; set; }

        public bool SelectedForDeletion { get; set; }
    }
}
