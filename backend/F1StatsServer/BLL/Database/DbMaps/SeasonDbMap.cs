using F1StatsServer.Entities.Seasons;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F1StatsServer.BLL.Database.DbMaps;

public class SeasonDbMap : BaseDbMap<Season>
{
    protected override string Table => "season";

    protected override void Map(EntityTypeBuilder<Season> builder)
    {
        base.Map(builder);

        builder.HasOne(x => x.League)
            .WithMany(l => l.Seasons)
            .HasForeignKey(x => x.LeagueId);

        builder.HasOne(x => x.LogoResource)
            .WithMany()
            .HasForeignKey(x => x.LogoResourceId);
    }
}
