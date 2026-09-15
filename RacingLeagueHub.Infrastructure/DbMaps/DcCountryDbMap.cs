using RacingLeagueHub.Domain.DataCatalogs;

namespace RacingLeagueHub.Infrastructure.DbMaps;

internal class DcCountryDbMap : DbMapBase<DcCountry>
{
    protected override string Table => "dc_country";
}
