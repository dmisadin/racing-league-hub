using RacingLeagueHub.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RacingLeagueHub.Infrastructure.DbMaps
{
    public class UserDbMap : DbMapBase<User>
    {
        protected override string Table => "user";

        protected override void Map(EntityTypeBuilder<User> builder)
        {
            base.Map(builder);

            builder.HasOne(x => x.Driver)
                .WithOne(d => d.User)
                .HasForeignKey<User>(x => x.DriverId);
        }
    }
}
