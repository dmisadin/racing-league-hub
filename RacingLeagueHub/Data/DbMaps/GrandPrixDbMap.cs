using RacingLeagueHub.BLL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RacingLeagueHub.Data.DbMaps;

public class GrandPrixDbMap : DbMapBase<GrandPrix>
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
