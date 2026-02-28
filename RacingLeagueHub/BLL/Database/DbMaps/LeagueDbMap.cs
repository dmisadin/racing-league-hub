using RacingLeagueHub.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RacingLeagueHub.BLL.Database.DbMaps
{
    public class LeagueDbMap : DbMapBase<League>
    {
        protected override string Table => "league";

        protected override void Map(EntityTypeBuilder<League> builder)
        {
            base.Map(builder);

            builder.HasOne(x => x.LogoResource)
                .WithMany()
                .HasForeignKey(x => x.LogoResourceId);

            builder.HasMany(x => x.LeagueUsers)
                .WithOne(x => x.League)
                .HasForeignKey(x => x.LeagueId);
        }
    }
}
