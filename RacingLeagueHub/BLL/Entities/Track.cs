namespace RacingLeagueHub.BLL.Entities;

public class Track : EntityBase
{
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public decimal? Elevation { get; set; }
    public string? ShortName { get; set; }

    public ICollection<TrackLayout> TrackLayouts { get; set; }
}

