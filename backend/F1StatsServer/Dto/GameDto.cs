using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace F1StatsServer.Dto
{
    public class GameDto
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}
