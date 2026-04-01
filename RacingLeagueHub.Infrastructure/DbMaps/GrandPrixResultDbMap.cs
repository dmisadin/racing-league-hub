using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacingLeagueHub.Domain.Entities.GrandsPrix;

namespace RacingLeagueHub.Infrastructure.DbMaps;

public class GrandPrixResultDbMap : DbMapBase<GrandPrixResult>
{
    protected override string Table => "grand_prix_result";

    protected override void Map(EntityTypeBuilder<GrandPrixResult> builder)
    {
        base.Map(builder);

        builder.HasOne(x => x.GrandPrixDriver)
            .WithMany(gpd => gpd.GrandPrixResults)
            .HasForeignKey(x => x.GrandPrixDriverId);
    }
}
