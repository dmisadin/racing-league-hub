using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace F1StatsServer.Dto.ResultsDtos
{
    public class RaceDto : RaceSprintDto
    {
        public int? FastestLapInMs { get; set; }

        public short? PostRaceTimePenalty { get; set; }
    }
}
