using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacingLeagueHub.Domain.Leagues.Seasons;

namespace RacingLeagueHub.Infrastructure.Persistence.Seasons.DbMaps;

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
