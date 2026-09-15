using RacingLeagueHub.Domain.Common.Entitites;
using RacingLeagueHub.Domain.DataCatalogs;

namespace RacingLeagueHub.Domain.Tracks;

public class Track : EntityBase
{
    public string Name { get; set; }
    public int CountryId { get; set; }
    public string City { get; set; }
    public string? ShortName { get; set; }

    public virtual DcCountry Country { get; set; }

    public virtual ICollection<TrackLayout> TrackLayouts { get; set; }
}

