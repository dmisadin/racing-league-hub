using RacingLeagueHub.Domain.DataCatalogs;

namespace RacingLeagueHub.Infrastructure.Persistence.DataCatalogs;

internal class DcCountryDbMap : DbMapBase<DcCountry>
{
    protected override string Table => "dc_country";
}
