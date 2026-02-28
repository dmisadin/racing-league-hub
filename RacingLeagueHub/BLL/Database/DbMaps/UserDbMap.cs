using F1StatsServer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F1StatsServer.BLL.Database.DbMaps
{
    public class UserDbMap : DbMapBase<User>
    {
        protected override string Table => "user";

        protected override void Map(EntityTypeBuilder<User> builder)
        {
            base.Map(builder);

            builder.HasOne(x => x.Driver)
                .WithOne()
                .HasForeignKey<User>(x => x.DriverId);
        }
    }
}
