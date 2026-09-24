using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacingLeagueHub.Domain.Leagues.Seasons;

namespace RacingLeagueHub.Infrastructure.Persistence.Seasons.DbMaps;

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
