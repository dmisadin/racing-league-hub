using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacingLeagueHub.Domain.Leagues.Seasons;

namespace RacingLeagueHub.Infrastructure.Persistence.Seasons.DbMaps;

internal class SeasonDivisionDbMap : DbMapBase<SeasonDivision>
{
    protected override string Table => "season_division";

    protected override void Map(EntityTypeBuilder<SeasonDivision> builder)
    {
        base.Map(builder);

        builder.HasOne(x => x.Season)
            .WithMany(s => s.SeasonDivisions)
            .HasForeignKey(x => x.SeasonId);

        builder.HasOne(x => x.Division)
            .WithMany(d => d.SeasonDivisions)
            .HasForeignKey(x => x.DivisionId);

        builder.HasIndex(x => new { x.SeasonId, x.DivisionId })
            .IsUnique();
    }
}
