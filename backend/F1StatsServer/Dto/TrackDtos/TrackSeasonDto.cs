using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace F1StatsServer.Dto.TrackDtos
{
    public class TrackSeasonDto
    {
        public int? Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Location { get; set; }

        public string? ImagePath { get; set; }

        [StringLength(2)]
        [Unicode(false)]
        public string? CountryIso { get; set; }
    }
}
