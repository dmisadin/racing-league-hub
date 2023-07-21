using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace F1StatsServer.Dto.SeasonDtos
{
    public class SeasonAssistDto
    {
        [StringLength(16)]
        [Unicode(false)]
        public string RacingLine { get; set; } = null!;

        [StringLength(10)]
        [Unicode(false)]
        public string Gearbox { get; set; } = null!;

        [StringLength(10)]
        [Unicode(false)]
        public string TractionControl { get; set; } = null!;

        [Column("ABS")]
        public bool Abs { get; set; }
    }
}
