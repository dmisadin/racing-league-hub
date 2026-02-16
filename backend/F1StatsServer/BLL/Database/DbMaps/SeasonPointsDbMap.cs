using F1StatsServer.Entities.Seasons;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F1StatsServer.BLL.Database.DbMaps;

public class SeasonPointsDbMap : BaseDbMap<SeasonPoints>
{
    protected override string Table => "season_points";

    protected override void Map(EntityTypeBuilder<SeasonPoints> builder)
    {
        base.Map(builder);

        builder.HasOne(x => x.Season)
            .WithMany(s => s.SeasonPoints)
            .HasForeignKey(x => x.SeasonId);
    }
}
