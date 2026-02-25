namespace F1StatsServer.Entities;

public class GrandPrixDriver : EntityBase
{
    public long GrandPrixId { get; set; }
    public long DriverId { get; set; }
    public long TeamId { get; set; }
    public bool IsReserve { get; set; }

    public virtual GrandPrix GrandPrix { get; set; }
    public virtual Driver Driver { get; set; }
    public virtual Team Team { get; set; }

    public virtual ICollection<GrandPrixResult> GrandPrixResults { get; set; }
}
