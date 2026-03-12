using RacingLeagueHub.BLL.Entities.Stewarding;

namespace RacingLeagueHub.BLL.Entities;

public class User : EntityBase
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
    public long? DriverId { get; set; }

    public virtual Driver? Driver { get; set; }

    public virtual ICollection<LeagueUser> LeagueUsers { get; set; }
    public virtual ICollection<Incident> Incidents { get; set; }
}

