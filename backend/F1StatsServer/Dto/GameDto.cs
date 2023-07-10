using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace F1StatsServer.Dto
{
    public class GameDto
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
