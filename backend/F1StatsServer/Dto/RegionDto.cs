using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace F1StatsServer.Dto
{
    public class RegionDto
    {
        [Required, NotNull]
        public string Name { get; set; }
    }
}
