using RacingLeagueHub.BLL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RacingLeagueHub.Data.DbMaps;

public class TrackLayoutDbMap : DbMapBase<TrackLayout>
{
    protected override string Table => "track_layout";

    protected override void Map(EntityTypeBuilder<TrackLayout> builder)
    {
        base.Map(builder);

        builder.HasOne(x => x.Track)
            .WithMany(t => t.TrackLayouts)
            .HasForeignKey(x => x.TrackId);

        builder.HasOne(x => x.MapImageResource)
            .WithMany()
            .HasForeignKey(x => x.MapImageResourceId);

        builder.HasOne(x => x.CoverImageResource)
            .WithMany()
            .HasForeignKey(x => x.CoverImageResourceId);
    }
}
