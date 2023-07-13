using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace F1StatsServer.Dto.DriverDto
{
    public class DriverDto
    {
        [Required]
        [NotNull]
        public string? Name { get; set; }
    }
}
