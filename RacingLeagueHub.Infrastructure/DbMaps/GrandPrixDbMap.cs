using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacingLeagueHub.Domain.Entities.GrandsPrix;

namespace RacingLeagueHub.Infrastructure.DbMaps;

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
