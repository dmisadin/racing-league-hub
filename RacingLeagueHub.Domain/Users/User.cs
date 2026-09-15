using RacingLeagueHub.Domain.Common.Entitites;
using RacingLeagueHub.Domain.Drivers;
using RacingLeagueHub.Domain.Leagues;
using RacingLeagueHub.Domain.Stewarding;

namespace RacingLeagueHub.Domain.Users;

public class User : EntityBase
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? DriverId { get; set; }

    public bool TwoFactorEnabled { get; set; }
    public string? TwoFactorSecret { get; set; }
    public DateTimeOffset? TwoFactorEnabledAt { get; set; }
    // Prevent accepting the same TOTP code twice in the same 30-second window.
    public long? LastTotpTimeStepUsed { get; set; }

    public virtual Driver? Driver { get; set; }

    public virtual ICollection<LeagueUser> LeagueUsers { get; set; }
    public virtual ICollection<Incident> Incidents { get; set; }
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    public virtual ICollection<UserRecoveryCode> UserRecoveryCodes { get; set; }
    public virtual ICollection<UserExternalLogin> ExternalLogins { get; set; } = [];
}

