using RacingLeagueHub.Domain.Resources;

namespace RacingLeagueHub.Infrastructure.Persistence.Resources;

public class ResourceDbMap : DbMapBase<Resource>
{
    protected override string Table => "resource";
}
