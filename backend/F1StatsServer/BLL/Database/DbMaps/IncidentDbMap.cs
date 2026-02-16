using F1StatsServer.Entities;
using F1StatsServer.Entities.Stewarding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace F1StatsServer.BLL.Database.DbMaps;

public class IncidentDbMap : BaseDbMap<Incident>
{
    protected override string Table => "incident";

    protected override void Map(EntityTypeBuilder<Incident> builder)
    {
        base.Map(builder);

        builder.HasOne(x => x.GrandPrix)
            .WithMany(gp => gp.Incidents)
            .HasForeignKey(x => x.GrandPrixId);

        builder.HasOne(x => x.User)
            .WithMany(u => u.Incidents)
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Verdicts)
            .WithOne()
            .HasForeignKey(v => v.Incident);

        builder.HasMany(i => i.Drivers)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "incident_driver",
                j => j.HasOne<Driver>()
                      .WithMany()
                      .HasForeignKey("driver_id")
                      .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Incident>()
                      .WithMany()
                      .HasForeignKey("incident_id")
                      .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.ToTable("incident_driver");
                    j.HasKey("incident_id", "driver_id");
                });
    }
}
