using F1StatsServer.Entities.Stewarding;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F1StatsServer.BLL.Database.DbMaps;

public class VerdictDbMap : DbMapBase<Verdict>
{
    protected override string Table => "verdict";

    protected override void Map(EntityTypeBuilder<Verdict> builder)
    {
        base.Map(builder);

        builder.HasOne(x => x.Incident)
            .WithMany(i => i.Verdicts)
            .HasForeignKey(x => x.IncidentId);
    }
}
