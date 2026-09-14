namespace RacingLeagueHub.Domain.Entities.GrandsPrix;

public class GrandPrixDriver : EntityBase
{
    public int GrandPrixId { get; set; }
    public int DriverId { get; set; }
    public int TeamId { get; set; }
    public bool IsReserve { get; set; }

    public virtual GrandPrix GrandPrix { get; set; }
    public virtual Driver Driver { get; set; }
    public virtual Team Team { get; set; }

    public virtual ICollection<GrandPrixResult> GrandPrixResults { get; set; }
}
