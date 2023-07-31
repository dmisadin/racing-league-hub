using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace F1StatsServer.Dto.ResultsDtos
{
    public class RaceSprintDto : ResultDto
    {
        public decimal? RaceTime { get; set; }

        public byte? TimePenalty { get; set; }

        public byte? LapsCompleted { get; set; }

        public byte? GridPosition { get; set; }

        [StringLength(8)]
        [Unicode(false)]
        public string? UsedTyres { get; set; }

    }
}
