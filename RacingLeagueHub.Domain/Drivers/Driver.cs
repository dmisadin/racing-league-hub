using RacingLeagueHub.Domain.Common.Entitites;
using RacingLeagueHub.Domain.DataCatalogs;
using RacingLeagueHub.Domain.Seasons;
using RacingLeagueHub.Domain.Users;

namespace RacingLeagueHub.Domain.Drivers;

public partial class Driver : EntityBase
{
    public string Nickname { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? CountryId { get; set; }
    public string Slug { get; set; }

    public virtual User User { get; set; }
    public virtual DcCountry? Country { get; set; }

    public virtual ICollection<SeasonDriver> SeasonDrivers { get; set; }
}
