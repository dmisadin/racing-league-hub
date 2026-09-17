using RacingLeagueHub.Domain.Common.Entitites;
using RacingLeagueHub.Domain.GrandsPrix;
using RacingLeagueHub.Domain.Leagues;
using RacingLeagueHub.Domain.Models.Enums;
using RacingLeagueHub.Domain.Resources;

namespace RacingLeagueHub.Domain.Leagues.Seasons;

public class Season : EntityBase
{
    public int LeagueId { get; set; }
    public string Name { get; set; }
    public Platform Platform { get; set; }
    public Game Game { get; set; }
    public short LapPercentageRequired { get; set; }
    public string Slug { get; set; }
    public int? LogoResourceId { get; set; }

    public virtual League League { get; set; }
    public virtual Resource? LogoResource { get; set; }

    public virtual ICollection<GrandPrix> GrandsPrix { get; set; }
    public virtual ICollection<SeasonAssists> SeasonAssists { get; set; }
    public virtual ICollection<SeasonLobbySettings> SeasonLobbySettings { get; set; }
    public virtual ICollection<SeasonPoints> SeasonPoints { get; set; }
    public virtual ICollection<SeasonDriver> SeasonDrivers { get; set; }
    public virtual ICollection<SeasonDivision> SeasonDivisions { get; set; }
}
