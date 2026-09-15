using RacingLeagueHub.Domain.Common.Entitites;
using RacingLeagueHub.Domain.Users;

namespace RacingLeagueHub.Domain.Leagues;

public class LeagueUser : EntityBase
{
    public int LeagueId { get; set; }
    public int UserId { get; set; }
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
