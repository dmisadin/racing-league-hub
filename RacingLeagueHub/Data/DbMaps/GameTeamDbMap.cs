using RacingLeagueHub.BLL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RacingLeagueHub.Data.DbMaps;

public class GameTeamDbMap : DbMapBase<GameTeam>
{
    protected override string Table => "game_team";

    protected override void Map(EntityTypeBuilder<GameTeam> builder)
    {
        base.Map(builder);

        builder.HasOne(x => x.Team)
            .WithMany(t => t.GameTeams)
            .HasForeignKey(x => x.TeamId);

        builder.HasOne(x => x.LogoResource)
            .WithMany()
            .HasForeignKey(x => x.LogoResourceId);
    }
}
