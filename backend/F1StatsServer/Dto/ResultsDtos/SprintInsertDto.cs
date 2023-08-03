using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace F1StatsServer.Dto.ResultsDtos
{
    public class SprintInsertDto : ResultSeasonDto
    {
        public decimal? RaceTime { get; set; }

        public byte? TimePenalty { get; set; }

        public byte? LapsCompleted { get; set; }

        public byte? GridPosition { get; set; }

        [StringLength(8)]
        [Unicode(false)]
        public string? UsedTyres { get; set; }

        public byte? Position { get; set; }

        public bool? IsReserve { get; set; }
    }
}
