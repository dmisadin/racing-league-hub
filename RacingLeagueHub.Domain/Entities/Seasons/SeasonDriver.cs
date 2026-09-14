namespace RacingLeagueHub.Domain.Entities.Seasons;

public class SeasonDriver : EntityBase
{
    public int SeasonId { get; set; }
    public int TeamId { get; set; }
    public int DriverId { get; set; }
    public short? RacingNumber { get; set; }
    public short PenaltyPoints { get; set; }

    public virtual Season Season { get; set; }
    public virtual Team Team { get; set; }
    public virtual Driver Driver { get; set; }
}
