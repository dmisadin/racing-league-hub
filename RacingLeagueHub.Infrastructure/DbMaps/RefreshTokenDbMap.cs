using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.DbMaps;

public class RefreshTokenDbMap : DbMapBase<RefreshToken>
{
    protected override string Table => "refresh_token";

    protected override void Map(EntityTypeBuilder<RefreshToken> builder)
    {
        base.Map(builder);

        builder.Property(x => x.Token).IsRequired().HasMaxLength(256);
        builder.Property(x => x.ExpiresAt).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.IsRevoked).IsRequired().HasDefaultValue(false);

        builder.Ignore(x => x.IsExpired);
        builder.Ignore(x => x.IsActive);

        builder.HasIndex(x => x.Token).IsUnique();
        builder.HasIndex(x => x.UserId);
    }
}