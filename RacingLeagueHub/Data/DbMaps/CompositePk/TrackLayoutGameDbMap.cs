using RacingLeagueHub.BLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacingLeagueHub.Data.DbMaps;

namespace RacingLeagueHub.Data.DbMaps.CompositePk;

public class TrackLayoutGameDbMap : IDbMap
{
    private string Schema => "public";
    private string Table => "track_layout_game";

    public virtual void Initialize(ModelBuilder modelBuilder)
    {
        var entityTypeBuilder = modelBuilder.Entity<TrackLayoutGame>();

        var builder = entityTypeBuilder.ToTable(this.Table.ToLower(), this.Schema.ToLower())
                                       .HasKey(x => new { x.TrackLayoutId, x.Game });

        Map(entityTypeBuilder);
    }
    private void Map(EntityTypeBuilder<TrackLayoutGame> builder)
    {
        builder.HasOne(x => x.TrackLayout)
            .WithMany(tl => tl.TrackLayoutGames)
            .HasForeignKey(x => x.TrackLayoutId);
    }
}
