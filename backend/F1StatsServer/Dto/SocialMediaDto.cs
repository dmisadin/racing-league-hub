using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace F1StatsServer.Dto
{
    public class SocialMediaDto
    {
        [StringLength(255)]
        [Unicode(false)]
        public string? Website { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public string? Discord { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public string? Youtube { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public string? Twitch { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public string? Facebook { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public string? Instagram { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public string? Twitter { get; set; }
    }
}
