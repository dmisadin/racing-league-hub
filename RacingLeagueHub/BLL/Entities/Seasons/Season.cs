using RacingLeagueHub.BLL.Entities.GrandsPrix;
using RacingLeagueHub.BLL.Entities.Resources;

namespace RacingLeagueHub.BLL.Entities.Seasons;

public class Season : EntityBase
{
    public long LeagueId { get; set; }
    public string Name { get; set; }
    public short Platform { get; set; }
    public short Game { get; set; }
    public short LapPercentageRequired { get; set; }
    public string Slug { get; set; }
    public long? LogoResourceId { get; set; }

    public virtual League League { get; set; }
    public virtual Resource? LogoResource { get; set; }

    public virtual ICollection<GrandPrix> GrandsPrix { get; set; }
    public virtual ICollection<SeasonAssists> SeasonAssists { get; set; }
    public virtual ICollection<SeasonLobbySettings> SeasonLobbySettings { get; set; }
    public virtual ICollection<SeasonPoints> SeasonPoints { get; set; }
    public virtual ICollection<SeasonDriver> SeasonDrivers { get; set; }
}
