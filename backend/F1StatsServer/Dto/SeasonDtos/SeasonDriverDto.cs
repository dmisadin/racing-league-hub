using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace F1StatsServer.Dto.SeasonDtos
{
    public class SeasonDriverDto
    {
        public int? Id { get; set; }

        [StringLength(40)]
        public string Name { get; set; } = null!;

        public int? TeamId { get; set; }

        [StringLength(2)]
        [Unicode(false)]
        public string? CountryIso { get; set; }

        public byte PenaltyPoints { get; set; }

    }
}
