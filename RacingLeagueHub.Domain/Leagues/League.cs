using RacingLeagueHub.Domain.Resources;
using RacingLeagueHub.Domain.Common.Entitites;
using RacingLeagueHub.Domain.Leagues.Seasons;
using RacingLeagueHub.Domain.Leagues.Divisions;
using RacingLeagueHub.Domain.Common.Models.Enums;

namespace RacingLeagueHub.Domain.Leagues;

public class League : EntityBase
{
    public Region Region { get; set; }
    public string Name { get; set; }
    public string Abbreviation { get; set; }
    public string? Description { get; set; }
    public string Timezone { get; set; }
    public string Slug { get; set; }
    public int? LogoResourceId { get; set; }

    public virtual Resource? LogoResource { get; set; }

    public virtual ICollection<LeagueUser> LeagueUsers { get; set; }
    public virtual ICollection<Season> Seasons { get; set; }
    public virtual ICollection<Division> Divisions { get; set; }
}