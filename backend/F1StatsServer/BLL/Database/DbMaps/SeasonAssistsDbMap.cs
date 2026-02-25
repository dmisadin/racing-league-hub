using F1StatsServer.Entities.Seasons;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F1StatsServer.BLL.Database.DbMaps;

public class SeasonAssistsDbMap : DbMapBase<SeasonAssists>
{
    protected override string Table => "season_assists";

    protected override void Map(EntityTypeBuilder<SeasonAssists> builder)
    {
        base.Map(builder);

        builder.HasOne(x => x.Season)
            .WithMany(s => s.SeasonAssists)
            .HasForeignKey(x => x.SeasonId);
    }
}
