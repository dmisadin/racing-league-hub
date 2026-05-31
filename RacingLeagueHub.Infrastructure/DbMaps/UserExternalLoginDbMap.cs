using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.DbMaps;

internal class UserExternalLoginDbMap : DbMapBase<UserExternalLogin>
{
    protected override string Table => "user_external_login";

    protected override void Map(EntityTypeBuilder<UserExternalLogin> builder)
    {
        base.Map(builder);

        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(x => x.Provider)
            .HasColumnName("provider")
            .IsRequired();

        builder.Property(x => x.ProviderUserId)
            .HasColumnName("provider_user_id")
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnName("email");

        builder.Property(x => x.DisplayName)
            .HasColumnName("display_name");

        builder.Property(x => x.PictureUrl)
            .HasColumnName("picture_url");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.ExternalLogins)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);

        builder.HasIndex(x => new { x.Provider, x.ProviderUserId })
            .IsUnique();

        builder.HasIndex(x => x.Email);
    }
}
