using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace F1StatsServer.Dto.ResultsDtos
{
    public class ResultDto : ResultSeasonDto
    {
        public int? Id { get; set; }

        public byte? Position { get; set; }

        public bool? IsReserve { get; set; }

        public string? TeamName { get; set; }

        public string? DriverName { get; set; }

        [StringLength(2)]
        [Unicode(false)]
        public string? DriverCountry { get; set; }
    }
}
