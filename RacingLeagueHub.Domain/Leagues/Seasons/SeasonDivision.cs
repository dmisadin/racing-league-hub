using RacingLeagueHub.Domain.Common.Entitites;
using RacingLeagueHub.Domain.GrandsPrix;
using RacingLeagueHub.Domain.Leagues.Divisions;

namespace RacingLeagueHub.Domain.Leagues.Seasons;

public class SeasonDivision : EntityBase
{
    public int SeasonId { get; set; }
    public int DivisionId { get; set; }

    public virtual Season Season { get; set; }
    public virtual Division Division { get; set; }

    public virtual ICollection<GrandPrix> GrandsPrix { get; set; }
    public virtual ICollection<SeasonDivisionAssists> Assists { get; set; }
    public virtual ICollection<SeasonDivisionLobbySettings> LobbySettings { get; set; }
    public virtual ICollection<SeasonDivisionPoints> Points { get; set; }
    public virtual ICollection<SeasonDivisionDriver> Drivers { get; set; }
}
