using RacingLeagueHub.Entities.Seasons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RacingLeagueHub.BLL.Database.DbMaps;

public class SeasonDriverDbMap : DbMapBase<SeasonDriver>
{
    protected override string Table => "season_driver";

    protected override void Map(EntityTypeBuilder<SeasonDriver> builder)
    {
        base.Map(builder);

        builder.HasOne(x => x.Season)
            .WithMany(s => s.SeasonDrivers)
            .HasForeignKey(x => x.SeasonId);

        builder.HasOne(x => x.Driver)
            .WithMany(d => d.SeasonDrivers)
            .HasForeignKey(x => x.DriverId);

        builder.HasOne(x => x.Team)
            .WithMany(t => t.SeasonDrivers)
            .HasForeignKey(x => x.TeamId);
    }
}
