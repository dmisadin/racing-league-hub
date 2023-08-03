using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace F1StatsServer.Dto.ResultsDtos
{
    public class ResultInsertDto
    {
        [Required, NotNull]
        public List<RaceInsertDto> Races { get; set; } = null!;

        [Required, NotNull]
        public List<QualInsertDto> Quals { get; set; } = null!;

        public List<SprintInsertDto>? Sprints { get; set; }
    }
}
