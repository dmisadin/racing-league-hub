using RacingLeagueHub.BLL.Models.Enums;

namespace RacingLeagueHub.BLL.Entities.Stewarding;

public class Incident : EntityBase
{
    public long UserId { get; set; }
    public long GrandPrixId { get; set; }
    public SessionType SessionType { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Evidence { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public virtual User User { get; set; }
    public virtual GrandPrix GrandPrix { get; set; }

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();
    public virtual ICollection<Verdict> Verdicts { get; set; } = new List<Verdict>();
}