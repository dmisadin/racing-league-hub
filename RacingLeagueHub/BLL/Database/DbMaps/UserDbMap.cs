using RacingLeagueHub.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RacingLeagueHub.BLL.Database.DbMaps
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
