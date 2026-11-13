using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacingLeagueHub.Domain.Leagues.Seasons;

namespace RacingLeagueHub.Infrastructure.Persistence.Seasons.DbMaps;

public class SeasonDivisionAssistsDbMap : DbMapBase<SeasonDivisionAssists>
{
    protected override string Table => "season_division_assists";

    protected override void Map(EntityTypeBuilder<SeasonDivisionAssists> builder)
    {
        base.Map(builder);

        builder.HasOne(x => x.SeasonDivision)
            .WithMany(sd => sd.Assists)
            .HasForeignKey(x => x.SeasonDivisionId);
    }
}
