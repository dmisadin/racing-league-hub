using RacingLeagueHub.Domain.Common.Entitites;

namespace RacingLeagueHub.Domain.Users;

public class PasswordResetToken : EntityBase
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsUsed { get; set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public bool IsActive => !IsUsed && !IsExpired;

    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
}