using F1StatsServer.Model.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace F1StatsServer.Dto.SeasonDtos
{
    public class SeasonsInLeagueDto
    {
        public int Id { get; set; }
        public Game? Game { get; set; }

        public Platform? Platform { get; set; }

        [StringLength(255)]
        public string Name { get; set; } = null!;

        [StringLength(255)]
        [Unicode(false)]
        public string? ImagePath { get; set; }

        public DateTimeOffset? StartTime { get; set; }

        public DateTimeOffset? EndTime { get; set; }
    }
}
