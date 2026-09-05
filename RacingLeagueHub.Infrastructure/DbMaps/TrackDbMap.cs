using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.DbMaps;

public class TrackDbMap : DbMapBase<Track>
{
    protected override string Table => "track";

    protected override void Map(EntityTypeBuilder<Track> builder)
    {
        base.Map(builder);

        builder.HasOne(x => x.Country)
            .WithMany()
            .HasForeignKey(x => x.CountryId);
    }
}
