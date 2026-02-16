using F1StatsServer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F1StatsServer.BLL.Database.DbMaps;

public class GrandPrixDbMap : BaseDbMap<GrandPrix>
{
    protected override string Table => "grand_prix";

    protected override void Map(EntityTypeBuilder<GrandPrix> builder)
    {
        base.Map(builder);

        builder.HasOne(x => x.Season)
            .WithMany(s => s.GrandsPrix)
            .HasForeignKey(x => x.SeasonId);

        builder.HasOne(x => x.TrackLayout)
            .WithMany()
            .HasForeignKey(x => x.TrackLayoutId);
    }
}
