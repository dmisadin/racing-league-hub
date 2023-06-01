using System;
using System.Collections.Generic;
using F1StatsServer.Model;
using Microsoft.EntityFrameworkCore;

namespace F1StatsServer.Data;

public partial class AdventureContext : DbContext
{
    public AdventureContext()
    {
    }

    public AdventureContext(DbContextOptions<AdventureContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<DriverCountry> DriverCountries { get; set; }

    public virtual DbSet<GrandPrix> GrandPrixes { get; set; }

    public virtual DbSet<GrandPrixCountry> GrandPrixCountries { get; set; }

    public virtual DbSet<GrandPrixTrack> GrandPrixTracks { get; set; }

    public virtual DbSet<League> Leagues { get; set; }

    public virtual DbSet<Qualifying> Qualifyings { get; set; }

    public virtual DbSet<Race> Races { get; set; }

    public virtual DbSet<Season> Seasons { get; set; }

    public virtual DbSet<SeasonDriver> SeasonDrivers { get; set; }

    public virtual DbSet<SeasonFastestLapPoint> SeasonFastestLapPoints { get; set; }

    public virtual DbSet<SeasonQualPoint> SeasonQualPoints { get; set; }

    public virtual DbSet<SeasonRacePoint> SeasonRacePoints { get; set; }

    public virtual DbSet<SeasonSprintPoint> SeasonSprintPoints { get; set; }

    public virtual DbSet<Sprint> Sprints { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<TrackCountry> TrackCountries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\;Database=F1Updated;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Croatian_100_CI_AS");

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.PkCountryId).HasName("PK__Country__0A4C9D57EBF5FE6C");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.PkDriverId).HasName("PK__Driver__0B1576E3D32BD147");
        });

        modelBuilder.Entity<DriverCountry>(entity =>
        {
            entity.HasOne(d => d.FkDriverCountryCountry).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("drivercountry_fk_drivercountry_countryid_foreign");

            entity.HasOne(d => d.FkDriverCountryDriver).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("drivercountry_fk_drivercountry_driverid_foreign");
        });

        modelBuilder.Entity<GrandPrix>(entity =>
        {
            entity.HasKey(e => e.PkGrandPrixId).HasName("PK__GrandPri__6782D7490BD0A25D");

            entity.HasOne(d => d.FkGrandPrixSeason).WithMany(p => p.GrandPrixes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("grandprix_fk_grandprix_seasonid_foreign");
        });

        modelBuilder.Entity<GrandPrixCountry>(entity =>
        {
            entity.HasOne(d => d.FkGrandPrixCountryCountry).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("grandprixcountry_fk_grandprixcountry_countryid_foreign");

            entity.HasOne(d => d.FkGrandPrixCountryGrandPrix).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("grandprixcountry_fk_grandprixcountry_grandprixid_foreign");
        });

        modelBuilder.Entity<GrandPrixTrack>(entity =>
        {
            entity.HasOne(d => d.FkGrandPrixTrackGrandPrix).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("grandprixtrack_fk_grandprixtrack_grandprixid_foreign");

            entity.HasOne(d => d.FkGrandPrixTrackTrack).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("grandprixtrack_fk_grandprixtrack_trackid_foreign");
        });

        modelBuilder.Entity<League>(entity =>
        {
            entity.HasKey(e => e.PkLeagueId).HasName("PK__League__231D6D700E807BE3");
        });

        modelBuilder.Entity<Qualifying>(entity =>
        {
            entity.HasOne(d => d.FkQualifyingDriver).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("qualifying_fk_qualifying_driverid_foreign");

            entity.HasOne(d => d.FkQualifyingGrandPrix).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("qualifying_fk_qualifying_grandprixid_foreign");

            entity.HasOne(d => d.FkQualifyingTeam).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("qualifying_fk_qualifying_teamid_foreign");
        });

        modelBuilder.Entity<Race>(entity =>
        {
            entity.HasOne(d => d.FkRaceDriver).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("race_fk_race_driverid_foreign");

            entity.HasOne(d => d.FkRaceGrandPrix).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("race_fk_race_grandprixid_foreign");

            entity.HasOne(d => d.FkRaceTeam).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("race_fk_race_teamid_foreign");
        });

        modelBuilder.Entity<Season>(entity =>
        {
            entity.HasKey(e => e.PkSeasonId).HasName("PK__Season__CADC9AEF26DA2C7D");

            entity.Property(e => e.LapsRequiredPercentage).HasDefaultValueSql("('90')");

            entity.HasOne(d => d.FkSeasonLeague).WithMany(p => p.Seasons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("season_fk_season_leagueid_foreign");
        });

        modelBuilder.Entity<SeasonDriver>(entity =>
        {
            entity.HasOne(d => d.FkSeasonDriverDriver).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("seasondriver_fk_seasondriver_driverid_foreign");

            entity.HasOne(d => d.FkSeasonDriverSeason).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("seasondriver_fk_seasondriver_seasonid_foreign");

            entity.HasOne(d => d.FkSeasonDriverTeam).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("seasondriver_fk_seasondriver_teamid_foreign");
        });

        modelBuilder.Entity<SeasonFastestLapPoint>(entity =>
        {
            entity.Property(e => e.FinishInsideTopN).HasDefaultValueSql("('10')");
            entity.Property(e => e.Points).HasDefaultValueSql("('1')");

            entity.HasOne(d => d.FkSeasonRacePointsSeason).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("seasonfastestlappoints_fk_seasonracepoints_seasonid_foreign");
        });

        modelBuilder.Entity<SeasonQualPoint>(entity =>
        {
            entity.HasOne(d => d.FkSeasonQualPointsSeason).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("seasonqualpoints_fk_seasonqualpoints_seasonid_foreign");
        });

        modelBuilder.Entity<SeasonRacePoint>(entity =>
        {
            entity.HasOne(d => d.FkSeasonRacePointsSeason).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("seasonracepoints_fk_seasonracepoints_seasonid_foreign");
        });

        modelBuilder.Entity<SeasonSprintPoint>(entity =>
        {
            entity.HasOne(d => d.FkSeasonSprintPointsSeason).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("seasonsprintpoints_fk_seasonsprintpoints_seasonid_foreign");
        });

        modelBuilder.Entity<Sprint>(entity =>
        {
            entity.HasOne(d => d.FkSprintDriverDriver).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sprint_fk_sprintdriver_driverid_foreign");

            entity.HasOne(d => d.FkSprintDriverGrandPrix).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sprint_fk_sprintdriver_grandprixid_foreign");

            entity.HasOne(d => d.FkSprintTeam).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sprint_fk_sprint_teamid_foreign");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.PkTeamId).HasName("PK__Team__A9F473E8250296B6");

            entity.Property(e => e.PkTeamId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(e => e.PkTrackId).HasName("PK__Track__7BB59AE5BC0020CC");

            entity.Property(e => e.Laps).HasDefaultValueSql("('52')");
        });

        modelBuilder.Entity<TrackCountry>(entity =>
        {
            entity.HasOne(d => d.FkTrackCountryCountry).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("trackcountry_fk_trackcountry_countryid_foreign");

            entity.HasOne(d => d.FkTrackCountryTrack).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("trackcountry_fk_trackcountry_trackid_foreign");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
