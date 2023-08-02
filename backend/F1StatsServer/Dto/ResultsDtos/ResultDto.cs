using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace F1StatsServer.Dto.ResultsDtos
{
    public class ResultDto : ResultSeasonDto
    {
        public int? Id { get; set; }

        public byte? Position { get; set; }

        public bool? IsReserve { get; set; }

    }
}
