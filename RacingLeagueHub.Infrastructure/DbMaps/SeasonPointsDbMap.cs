using RacingLeagueHub.Domain.Entities.Seasons;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RacingLeagueHub.Data.DbMaps;

public class SeasonPointsDbMap : DbMapBase<SeasonPoints>
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
