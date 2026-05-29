namespace RacingLeagueHub.Domain.Entities;

public class UserRecoveryCode : EntityBase
{
    public long UserId { get; set; }
    public string CodeHash { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UsedAt { get; set; }

    public User User { get; set; } = null!;
}