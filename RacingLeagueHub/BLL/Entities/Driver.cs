using RacingLeagueHub.BLL.Entities.Seasons;

namespace RacingLeagueHub.BLL.Entities;

public partial class Driver : EntityBase
{
    public string Nickname { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Country { get; set; }
    public string Slug { get; set; }

    public virtual User User { get; set; }

    public virtual ICollection<SeasonDriver> SeasonDrivers { get; set; }
}
