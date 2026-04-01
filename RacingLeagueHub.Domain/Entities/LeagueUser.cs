namespace RacingLeagueHub.BLL.Entities;

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
}
