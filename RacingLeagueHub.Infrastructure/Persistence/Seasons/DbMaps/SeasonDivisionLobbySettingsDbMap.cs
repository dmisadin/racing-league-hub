using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacingLeagueHub.Domain.Leagues.Seasons;

namespace RacingLeagueHub.Infrastructure.Persistence.Seasons.DbMaps;

public class SeasonDivisionLobbySettingsDbMap : DbMapBase<SeasonDivisionLobbySettings>
{
    protected override string Table => "season_division_lobby_settings";

    protected override void Map(EntityTypeBuilder<SeasonDivisionLobbySettings> builder)
    {
        base.Map(builder);

        builder.HasOne(x => x.SeasonDivision)
            .WithMany(sd => sd.LobbySettings)
            .HasForeignKey(x => x.SeasonDivisionId);
    }
}
