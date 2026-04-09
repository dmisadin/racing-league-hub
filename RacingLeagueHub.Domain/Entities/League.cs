using RacingLeagueHub.Domain.Models.Enums;
using RacingLeagueHub.Domain.Entities.Seasons;
using RacingLeagueHub.Domain.Entities.Resources;

namespace RacingLeagueHub.Domain.Entities;

public class League : EntityBase
{
    public Region Region { get; set; }
    public string Name { get; set; }
    public string Abbreviation { get; set; }
    public string? Description { get; set; }
    public string Timezone { get; set; }
    public string Slug { get; set; }
    public long? LogoResourceId { get; set; }

    public virtual Resource? LogoResource { get; set; }

    public virtual ICollection<LeagueUser> LeagueUsers { get; set; }
    public virtual ICollection<Season> Seasons { get; set; }
}