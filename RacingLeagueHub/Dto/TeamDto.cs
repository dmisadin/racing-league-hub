using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace F1StatsServer.Dto
{
    public class TeamDto
    {
        public int? Id { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public string Name { get; set; } = null!;

        [StringLength(255)]
        [Unicode(false)]
        public string? ImagePath { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string ColorHex { get; set; } = null!;
    }
}
