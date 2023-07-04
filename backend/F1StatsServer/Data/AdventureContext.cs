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

    public virtual DbSet<GrandPrix> GrandPrixes { get; set; }

    public virtual DbSet<League> Leagues { get; set; }

    public virtual DbSet<Qualifying> Qualifyings { get; set; }

    public virtual DbSet<Race> Races { get; set; }

    public virtual DbSet<Season> Seasons { get; set; }

    public virtual DbSet<SeasonAssist> SeasonAssists { get; set; }

    public virtual DbSet<SeasonDriver> SeasonDrivers { get; set; }

    public virtual DbSet<SeasonFastestLapPoint> SeasonFastestLapPoints { get; set; }

    public virtual DbSet<SeasonLobbySetting> SeasonLobbySettings { get; set; }

    public virtual DbSet<SeasonQualPoint> SeasonQualPoints { get; set; }

    public virtual DbSet<SeasonRacePoint> SeasonRacePoints { get; set; }

    public virtual DbSet<SeasonSprintPoint> SeasonSprintPoints { get; set; }

    public virtual DbSet<Sprint> Sprints { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\;Database=F1Stats;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Croatian_CI_AS");

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.PkCountryId).HasName("PK__Country__0A4C9D574FA9C4D1");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.PkDriverId).HasName("PK__Driver__0B1576E3B0855E85");

            entity.HasMany(d => d.FkDriverCountryCountries).WithMany(p => p.FkDriverCountryDrivers)
                .UsingEntity<Dictionary<string, object>>(
                    "DriverCountry",
                    r => r.HasOne<Country>().WithMany()
                        .HasForeignKey("FkDriverCountryCountryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("drivercountry_fk_drivercountry_countryid_foreign"),
                    l => l.HasOne<Driver>().WithMany()
                        .HasForeignKey("FkDriverCountryDriverId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("drivercountry_fk_drivercountry_driverid_foreign"),
                    j =>
                    {
                        j.HasKey("FkDriverCountryDriverId", "FkDriverCountryCountryId");
                        j.ToTable("DriverCountry");
                        j.IndexerProperty<int>("FkDriverCountryDriverId").HasColumnName("FK_DriverCountry_DriverId");
                        j.IndexerProperty<short>("FkDriverCountryCountryId").HasColumnName("FK_DriverCountry_CountryId");
                    });
        });

        modelBuilder.Entity<GrandPrix>(entity =>
        {
            entity.HasKey(e => e.PkGrandPrixId).HasName("PK__GrandPri__6782D749A38250B4");

            entity.HasOne(d => d.FkGrandPrixSeason).WithMany(p => p.GrandPrixes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("grandprix_fk_grandprix_seasonid_foreign");

            entity.HasMany(d => d.FkGrandPrixCountryCountries).WithMany(p => p.FkGrandPrixCountryGrandPrixes)
                .UsingEntity<Dictionary<string, object>>(
                    "GrandPrixCountry",
                    r => r.HasOne<Country>().WithMany()
                        .HasForeignKey("FkGrandPrixCountryCountryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("grandprixcountry_fk_grandprixcountry_countryid_foreign"),
                    l => l.HasOne<GrandPrix>().WithMany()
                        .HasForeignKey("FkGrandPrixCountryGrandPrixId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("grandprixcountry_fk_grandprixcountry_grandprixid_foreign"),
                    j =>
                    {
                        j.HasKey("FkGrandPrixCountryGrandPrixId", "FkGrandPrixCountryCountryId");
                        j.ToTable("GrandPrixCountry");
                        j.IndexerProperty<int>("FkGrandPrixCountryGrandPrixId").HasColumnName("FK_GrandPrixCountry_GrandPrixId");
                        j.IndexerProperty<short>("FkGrandPrixCountryCountryId").HasColumnName("FK_GrandPrixCountry_CountryId");
                    });

            entity.HasMany(d => d.FkGrandPrixTrackTracks).WithMany(p => p.FkGrandPrixTrackGrandPrixes)
                .UsingEntity<Dictionary<string, object>>(
                    "GrandPrixTrack",
                    r => r.HasOne<Track>().WithMany()
                        .HasForeignKey("FkGrandPrixTrackTrackId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("grandprixtrack_fk_grandprixtrack_trackid_foreign"),
                    l => l.HasOne<GrandPrix>().WithMany()
                        .HasForeignKey("FkGrandPrixTrackGrandPrixId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("grandprixtrack_fk_grandprixtrack_grandprixid_foreign"),
                    j =>
                    {
                        j.HasKey("FkGrandPrixTrackGrandPrixId", "FkGrandPrixTrackTrackId");
                        j.ToTable("GrandPrixTrack");
                        j.IndexerProperty<int>("FkGrandPrixTrackGrandPrixId").HasColumnName("FK_GrandPrixTrack_GrandPrixId");
                        j.IndexerProperty<short>("FkGrandPrixTrackTrackId").HasColumnName("FK_GrandPrixTrack_TrackId");
                    });
        });

        modelBuilder.Entity<League>(entity =>
        {
            entity.HasKey(e => e.PkLeagueId).HasName("PK__League__231D6D70CBED4139");

            entity.HasOne(d => d.FkLeagueUser).WithMany(p => p.Leagues)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("league_fk_league_userid_foreign");
        });

        modelBuilder.Entity<Qualifying>(entity =>
        {
            entity.HasKey(e => e.PkQualifyingId).HasName("PK__Qualifyi__D1EE98B77B1B9789");

            entity.HasOne(d => d.FkQualifyingDriver).WithMany(p => p.Qualifyings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("qualifying_fk_qualifying_driverid_foreign");

            entity.HasOne(d => d.FkQualifyingGrandPrix).WithMany(p => p.Qualifyings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("qualifying_fk_qualifying_grandprixid_foreign");

            entity.HasOne(d => d.FkQualifyingTeam).WithMany(p => p.Qualifyings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("qualifying_fk_qualifying_teamid_foreign");
        });

        modelBuilder.Entity<Race>(entity =>
        {
            entity.HasKey(e => e.PkRaceId).HasName("PK__Race__47ACF371486D1AF2");

            entity.HasOne(d => d.FkRaceDriver).WithMany(p => p.Races)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("race_fk_race_driverid_foreign");

            entity.HasOne(d => d.FkRaceGrandPrix).WithMany(p => p.Races)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("race_fk_race_grandprixid_foreign");

            entity.HasOne(d => d.FkRaceTeam).WithMany(p => p.Races)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("race_fk_race_teamid_foreign");
        });

        modelBuilder.Entity<Season>(entity =>
        {
            entity.HasKey(e => e.PkSeasonId).HasName("PK__Season__CADC9AEF7EB0AB27");

            entity.Property(e => e.LapsRequiredPercentage).HasDefaultValueSql("('90')");

            entity.HasOne(d => d.FkSeasonLeague).WithMany(p => p.Seasons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("season_fk_season_leagueid_foreign");
        });

        modelBuilder.Entity<SeasonAssist>(entity =>
        {
            entity.HasOne(d => d.FkSeasonAssistsSeason).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("seasonassists_fk_seasonassists_seasonid_foreign");
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

        modelBuilder.Entity<SeasonLobbySetting>(entity =>
        {
            entity.HasOne(d => d.FkSeasonLobbySettingsSeason).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("seasonlobbysettings_fk_seasonlobbysettings_seasonid_foreign");
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
            entity.HasKey(e => e.PkSprintId).HasName("PK__Sprint__59782327CB7FAC7E");

            entity.HasOne(d => d.FkSprintDriverDriver).WithMany(p => p.Sprints)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sprint_fk_sprintdriver_driverid_foreign");

            entity.HasOne(d => d.FkSprintDriverGrandPrix).WithMany(p => p.Sprints)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sprint_fk_sprintdriver_grandprixid_foreign");

            entity.HasOne(d => d.FkSprintTeam).WithMany(p => p.Sprints)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sprint_fk_sprint_teamid_foreign");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.PkTeamId).HasName("PK__Team__A9F473E8454B909B");

            entity.Property(e => e.PkTeamId).ValueGeneratedOnAdd();
            entity.Property(e => e.ColorHex).HasDefaultValueSql("('#000')");
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(e => e.PkTrackId).HasName("PK__Track__7BB59AE5B5FBC206");

            entity.Property(e => e.Laps).HasDefaultValueSql("('52')");

            entity.HasMany(d => d.FkTrackCountryCountries).WithMany(p => p.FkTrackCountryTracks)
                .UsingEntity<Dictionary<string, object>>(
                    "TrackCountry",
                    r => r.HasOne<Country>().WithMany()
                        .HasForeignKey("FkTrackCountryCountryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("trackcountry_fk_trackcountry_countryid_foreign"),
                    l => l.HasOne<Track>().WithMany()
                        .HasForeignKey("FkTrackCountryTrackId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("trackcountry_fk_trackcountry_trackid_foreign"),
                    j =>
                    {
                        j.HasKey("FkTrackCountryTrackId", "FkTrackCountryCountryId");
                        j.ToTable("TrackCountry");
                        j.IndexerProperty<short>("FkTrackCountryTrackId").HasColumnName("FK_TrackCountry_TrackId");
                        j.IndexerProperty<short>("FkTrackCountryCountryId").HasColumnName("FK_TrackCountry_CountryId");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.PkUserId).HasName("PK__User__7C1FCE7FE9C46CDC");

            entity.HasMany(d => d.FkLeagueUserLeagues).WithMany(p => p.FkLeagueUserUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "LeagueUser",
                    r => r.HasOne<League>().WithMany()
                        .HasForeignKey("FkLeagueUserLeagueId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("leagueuser_fk_leagueuser_leagueid_foreign"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("FkLeagueUserUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("leagueuser_fk_leagueuser_userid_foreign"),
                    j =>
                    {
                        j.HasKey("FkLeagueUserUserId", "FkLeagueUserLeagueId");
                        j.ToTable("LeagueUser");
                        j.IndexerProperty<int>("FkLeagueUserUserId").HasColumnName("FK_LeagueUser_UserId");
                        j.IndexerProperty<int>("FkLeagueUserLeagueId").HasColumnName("FK_LeagueUser_LeagueId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
