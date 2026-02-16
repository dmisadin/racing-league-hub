using F1StatsServer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F1StatsServer.BLL.Database.DbMaps;

public class GrandPrixDriverDbMap : BaseDbMap<GrandPrixDriver>
{
    protected override string Table => "grand_prix_driver";

    protected override void Map(EntityTypeBuilder<GrandPrixDriver> builder)
    {
        base.Map(builder);

        builder.HasOne(x => x.GrandPrix)
            .WithMany()
            .HasForeignKey(x => x.GrandPrixId);

        builder.HasOne(x => x.Driver)
            .WithMany()
            .HasForeignKey(x => x.DriverId);

        builder.HasOne(x => x.Team)
            .WithMany(t => t.GrandPrixDrivers)
            .HasForeignKey(x => x.TeamId);
    }
}
