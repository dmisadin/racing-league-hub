using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace F1StatsServer.Dto.ResultsDtos
{
    public class RaceInsertDto : ResultSeasonDto
    {
        public byte? Position { get; set; }

        public int? FastestLapInMs { get; set; }

        public short? PostRaceTimePenalty { get; set; }

        public bool? IsReserve { get; set; }

        public decimal? RaceTime { get; set; }

        public byte? TimePenalty { get; set; }

        public byte? LapsCompleted { get; set; }

        public byte? GridPosition { get; set; }

        [StringLength(8)]
        [Unicode(false)]
        public string? UsedTyres { get; set; }
    }
}
