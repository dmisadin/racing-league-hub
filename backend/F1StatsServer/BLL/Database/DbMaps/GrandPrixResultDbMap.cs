using F1StatsServer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F1StatsServer.BLL.Database.DbMaps;

public class GrandPrixResultDbMap : BaseDbMap<GrandPrixResult>
{
    protected override string Table => "grand_prix";

    protected override void Map(EntityTypeBuilder<GrandPrixResult> builder)
    {
        base.Map(builder);

        builder.HasOne(x => x.GrandPrixDriver)
            .WithMany(gpd => gpd.GrandPrixResults)
            .HasForeignKey(x => x.GrandPrixDriverId);
    }
}
