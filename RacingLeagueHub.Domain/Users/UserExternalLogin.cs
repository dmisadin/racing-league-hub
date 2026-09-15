using RacingLeagueHub.Domain.Common.Entitites;

namespace RacingLeagueHub.Domain.Users;

public class UserExternalLogin : EntityBase
{
    public int UserId { get; set; }

    public string Provider { get; set; } = null!;
    public string ProviderUserId { get; set; } = null!;

    public string? Email { get; set; }
    public string? DisplayName { get; set; }
    public string? PictureUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}