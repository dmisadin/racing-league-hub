using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace F1StatsServer.Dto.ResultsDtos
{
    public class QualDto : ResultDto
    {
        public int? FastestLapInMs { get; set; }

        [StringLength(2)]
        [Unicode(false)]
        public string? BestTimeTyre { get; set; }
    }
}
