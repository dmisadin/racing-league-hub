using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace F1StatsServer.Dto.GrandPrixDto
{
    public class GrandPrixInsertDto
    {
        public int SeasonId { get; set; }

        public int? TrackId { get; set; }

        public int? CountryId { get; set; }

        [StringLength(255)]
        public string Name { get; set; } = null!;

        //public DateTimeOffset StartTime { get; set; }

        public bool HasSprint { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public string? YouTubeUrl { get; set; }
    }
}
