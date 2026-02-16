using F1StatsServer.Entities.Seasons;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F1StatsServer.BLL.Database.DbMaps
{
    public class SeasonLobbySettingsDbMap : BaseDbMap<SeasonLobbySettings>
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
