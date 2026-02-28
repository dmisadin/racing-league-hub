using System.ComponentModel.DataAnnotations;

namespace RacingLeagueHub.Dto.DriverDtos
{
    public class DriverInsertDto
    {
        public int? PlatformId { get; set; }

        [StringLength(40)]
        public string Name { get; set; } = null!;

        public int? CountryId { get; set; }
    }
}
