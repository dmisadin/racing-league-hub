using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacingLeagueHub.Domain.Leagues;

namespace RacingLeagueHub.Infrastructure.Persistence.LeagueUsers;

public class LeagueUserDbMap : DbMapBase<LeagueUser>
{
    protected override string Table => "league_user";

    protected override void Map(EntityTypeBuilder<LeagueUser> builder)
    {
        base.Map(builder);

        builder.HasOne(x => x.League)
            .WithMany(l => l.LeagueUsers)
            .HasForeignKey(x => x.LeagueId);

        builder.HasOne(x => x.User)
            .WithMany(u => u.LeagueUsers)
            .HasForeignKey(x => x.UserId);
    }
}
