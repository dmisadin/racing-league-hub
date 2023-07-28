using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace F1StatsServer.Dto
{
    public class PlatformDto
    {
        [StringLength(255)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
    }
}
