using RacingLeagueHub.Entities;
using RacingLeagueHub.Entities.Stewarding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RacingLeagueHub.BLL.Database.DbMaps;

public class IncidentDbMap : DbMapBase<Incident>
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
