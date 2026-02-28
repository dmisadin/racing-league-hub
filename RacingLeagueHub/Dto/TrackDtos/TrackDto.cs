using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace F1StatsServer.Dto.TrackDtos
{
    public class TrackDto
    {
        public int? Id { get; set; }

        public CountryDto? Country { get; set; }

        public string? Name { get; set; }

        public string? Location { get; set; }

        public byte? CornersTotal { get; set; }

        public byte? CornersLeft { get; set; }

        public decimal? Elevation { get; set; }

        public short? Length { get; set; }

        public byte? PitStop { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public string? ImagePath { get; set; }

        public byte Laps { get; set; }

    }
}
