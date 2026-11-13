using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacingLeagueHub.Domain.Leagues.Seasons;

namespace RacingLeagueHub.Infrastructure.Persistence.Seasons.DbMaps;

public class SeasonDivisionPointsDbMap : DbMapBase<SeasonDivisionPoints>
{
    protected override string Table => "season_division_points";

    protected override void Map(EntityTypeBuilder<SeasonDivisionPoints> builder)
    {
        base.Map(builder);

        builder.HasOne(x => x.SeasonDivision)
            .WithMany(sd => sd.Points)
            .HasForeignKey(x => x.SeasonDivisionId);
    }
}
