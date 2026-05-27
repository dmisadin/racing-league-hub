using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.DbMaps;

internal class UserRecoveryCodeDbMap : DbMapBase<UserRecoveryCode>
{
    protected override string Table => "user_recovery_code";

    protected override void Map(EntityTypeBuilder<UserRecoveryCode> builder)
    {
        base.Map(builder);

        builder.HasOne(x => x.User)
            .WithMany(u => u.UserRecoveryCodes)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
