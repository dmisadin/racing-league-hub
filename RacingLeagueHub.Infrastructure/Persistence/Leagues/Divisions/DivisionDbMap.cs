using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacingLeagueHub.Domain.Leagues.Divisions;

namespace RacingLeagueHub.Infrastructure.Persistence.Leagues.Divisions;

internal class DivisionDbMap : DbMapBase<Division>
{
    protected override string Table => "division";

    protected override void Map(EntityTypeBuilder<Division> builder)
    {
        base.Map(builder);

        builder.HasOne(x => x.League)
            .WithMany(l => l.Divisions)
            .HasForeignKey(x => x.LeagueId);

        builder.HasIndex(x => new { x.LeagueId, x.Slug })
            .IsUnique();
    }
}
