namespace RacingLeagueHub.BLL.Entities.Seasons;

public class SeasonDriver : EntityBase
{
    public long SeasonId { get; set; }
    public long TeamId { get; set; }
    public long DriverId { get; set; }
    public short? RacingNumber { get; set; }
    public short PenaltyPoints { get; set; }

    public virtual Season Season { get; set; }
    public virtual Team Team { get; set; }
    public virtual Driver Driver { get; set; }
}
