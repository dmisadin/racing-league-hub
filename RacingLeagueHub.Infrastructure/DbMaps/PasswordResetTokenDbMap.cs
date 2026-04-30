using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacingLeagueHub.Domain.Entities.Authentication;

namespace RacingLeagueHub.Infrastructure.DbMaps;

public class PasswordResetTokenDbMap : DbMapBase<PasswordResetToken>
{
    protected override string Table => "password_reset_token";

    protected override void Map(EntityTypeBuilder<PasswordResetToken> builder)
    {
        base.Map(builder);
        
        builder.Ignore(x => x.IsExpired);
        builder.Ignore(x => x.IsActive);
        
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}