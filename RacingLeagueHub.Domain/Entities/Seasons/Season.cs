using RacingLeagueHub.Domain.Entities.GrandsPrix;
using RacingLeagueHub.Domain.Entities.Resources;
using RacingLeagueHub.Domain.Models.Enums;

namespace RacingLeagueHub.Domain.Entities.Seasons;

public class Season : EntityBase
{
    public long LeagueId { get; set; }
    public string Name { get; set; }
    public Platform Platform { get; set; }
    public Game Game { get; set; }
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
