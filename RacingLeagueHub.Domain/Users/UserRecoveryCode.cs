using RacingLeagueHub.Domain.Common.Entitites;

namespace RacingLeagueHub.Domain.Users;

public class UserRecoveryCode : EntityBase
{
    public int UserId { get; set; }
    public string CodeHash { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UsedAt { get; set; }

    public User User { get; set; } = null!;
}