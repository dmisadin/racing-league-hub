using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace F1StatsServer.Dto.DriverDto
{
    public class DriverSeasonDto
    {
        [StringLength(40)]
        public string Name { get; set; } = null!;

        [StringLength(255)]
        [Unicode(false)]
        public string TeamName { get; set; } = null!;

        [StringLength(255)]
        [Unicode(false)]
        public string? TeamImagePath { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string TeamColorHex { get; set; } = null!;

        [StringLength(2)]
        [Unicode(false)]
        public string? CountryIso { get; set; }

        public byte PenaltyPoints { get; set; }

    }
}
