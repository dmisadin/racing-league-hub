using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace F1StatsServer.Dto
{
    public class CountryDto
    {
        public int? Id { get; set; }

        [Required, NotNull]
        public string? NameEnglish { get; set; }

        [Required, NotNull]
        public string? NameCroatian { get; set; }

        [Required, NotNull, StringLength(2)]
        public string? Iso { get; set; }

        [Required, NotNull, StringLength(3)]
        public string? Iso3 { get; set; }

        [DefaultValue("/")]
        public string? ImagePath { get; set; }
    }
}
