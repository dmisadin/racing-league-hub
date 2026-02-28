using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RacingLeagueHub.Dto.SeasonDtos
{
    public class SeasonLobbySettingsDto
    {
        [StringLength(8)]
        [Unicode(false)]
        public string Qualifying { get; set; } = null!;

        public byte RaceDistancePercentage { get; set; }

        public bool FormationLap { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string Weather { get; set; } = null!;

        [StringLength(10)]
        [Unicode(false)]
        public string CornerCutting { get; set; } = null!;

        [StringLength(12)]
        [Unicode(false)]
        public string CarDamage { get; set; } = null!;

        [StringLength(12)]
        [Unicode(false)]
        public string CarDamageRate { get; set; } = null!;

        public bool ParcFerme { get; set; }

        public bool EqualCarPerformance { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string SafetyCar { get; set; } = null!;

        public bool Collisions { get; set; }

        public bool Ghosting { get; set; }

        [StringLength(15)]
        [Unicode(false), DefaultValue("Manual")]
        public string Start { get; set; } = null!;
    }
}
