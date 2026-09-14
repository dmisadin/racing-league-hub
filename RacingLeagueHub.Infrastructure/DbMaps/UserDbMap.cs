using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.DbMaps;

public class UserDbMap : DbMapBase<User>
{
    protected override string Table => "user";

    protected override void Map(EntityTypeBuilder<User> builder)
    {
        base.Map(builder);

        builder.HasOne(x => x.Driver)
            .WithOne(d => d.User)
            .HasForeignKey<User>(x => x.DriverId);

        builder.HasMany(x => x.RefreshTokens)
            .WithOne(rt => rt.User)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.UserRecoveryCodes)
            .WithOne(rt => rt.User)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
