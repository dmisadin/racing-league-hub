namespace RacingLeagueHub.Domain.Entities;

public class UserExternalLogin : EntityBase
{
    public long UserId { get; set; }

    public string Provider { get; set; } = null!;
    public string ProviderUserId { get; set; } = null!;

    public string? Email { get; set; }
    public string? DisplayName { get; set; }
    public string? PictureUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}