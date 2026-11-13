using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacingLeagueHub.Domain.Leagues.Seasons;

namespace RacingLeagueHub.Infrastructure.Persistence.Seasons.DbMaps;

public class SeasonDivisionDriverDbMap : DbMapBase<SeasonDivisionDriver>
{
    protected override string Table => "season_division_driver";

    protected override void Map(EntityTypeBuilder<SeasonDivisionDriver> builder)
    {
        base.Map(builder);

        builder.HasOne(x => x.SeasonDivision)
            .WithMany(sd => sd.Drivers)
            .HasForeignKey(x => x.SeasonDivisionId);

        builder.HasOne(x => x.Driver)
            .WithMany(d => d.SeasonDivisionDrivers)
            .HasForeignKey(x => x.DriverId);

        builder.HasOne(x => x.Team)
            .WithMany(t => t.SeasonDivisionDrivers)
            .HasForeignKey(x => x.TeamId);

        builder.HasIndex(x => new { x.SeasonDivisionId, x.DriverId, x.TeamId })
            .IsUnique();
    }
}
