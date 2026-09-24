using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacingLeagueHub.Domain.Leagues.Seasons;

namespace RacingLeagueHub.Infrastructure.Persistence.Seasons.DbMaps
{
    public class SeasonLobbySettingsDbMap : DbMapBase<SeasonLobbySettings>
    {
        protected override string Table => "season_lobby_settings";

        protected override void Map(EntityTypeBuilder<SeasonLobbySettings> builder)
        {
            base.Map(builder);

            builder.HasOne(x => x.Season)
                .WithMany(s => s.SeasonLobbySettings)
                .HasForeignKey(x => x.SeasonId);
        }
    }
}
