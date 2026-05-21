namespace RacingLeagueHub.Domain.Entities;

public class LeagueUser : EntityBase
{
    public long LeagueId { get; set; }
    public long UserId { get; set; }
    public bool IsOwner { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsEditor { get; set; }
    public bool IsSteward { get; set; }

    public virtual League League { get; set; }
    public virtual User User { get; set; }

    public bool CanManageLeague()
    {
        return IsOwner || IsAdmin;
    }

    public bool CanEditLeague()
    {
        return IsOwner || IsAdmin || IsEditor;
    }

    public bool CanStewardLeague()
    {
        return IsOwner || IsAdmin || IsSteward;
    }

    public bool CanOwnLeague()
    {
        return IsOwner;
    }
}
