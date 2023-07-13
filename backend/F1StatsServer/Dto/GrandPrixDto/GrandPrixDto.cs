using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace F1StatsServer.Dto.GrandPrixDto
{
    public class GrandPrixDto
    {
        [Required, NotNull]
        public int SeasonId { get; set; }
        [Required, NotNull]
        public string? Name { get; set; }
        //public DateTimeOffset Date { get; set; } //Will be added in the future
        [DefaultValue(false)]
        public bool HasSprint { get; set; }
        [DefaultValue("https://www.youtube.com")]
        public string? YoutubeUrl { get; set; }
    }
}
