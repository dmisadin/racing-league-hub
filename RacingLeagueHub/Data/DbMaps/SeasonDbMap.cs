using RacingLeagueHub.BLL.Entities.Seasons;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RacingLeagueHub.Data.DbMaps;

public class SeasonDbMap : DbMapBase<Season>
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
