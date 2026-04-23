using RacingLeagueHub.Domain.Entities.Stewarding;

namespace RacingLeagueHub.Domain.Entities;

public class User : EntityBase
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime CreatedAt { get; set; }
    public long? DriverId { get; set; }

    public virtual Driver? Driver { get; set; }

    public virtual ICollection<LeagueUser> LeagueUsers { get; set; }
    public virtual ICollection<Incident> Incidents { get; set; }
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
}

