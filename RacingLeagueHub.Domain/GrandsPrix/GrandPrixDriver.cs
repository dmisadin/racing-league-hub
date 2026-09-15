using RacingLeagueHub.Domain.Common.Entitites;
using RacingLeagueHub.Domain.Drivers;
using RacingLeagueHub.Domain.Teams;

namespace RacingLeagueHub.Domain.GrandsPrix;

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
