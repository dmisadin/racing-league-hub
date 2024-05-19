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

    public virtual DbSet<LeagueUser> LeagueUsers { get; set; }

    public virtual DbSet<Qualifying> Qualifyings { get; set; }

    public virtual DbSet<Race> Races { get; set; }

    public virtual DbSet<Season> Seasons { get; set; }

    public virtual DbSet<SeasonAssists> SeasonAssists { get; set; }

    public virtual DbSet<SeasonDrivers> SeasonDrivers { get; set; }

    public virtual DbSet<SeasonFastestLapPoints> SeasonFastestLapPoints { get; set; }

    public virtual DbSet<SeasonLobbySettings> SeasonLobbySettings { get; set; }

    public virtual DbSet<SeasonQualPoints> SeasonQualPoints { get; set; }

    public virtual DbSet<SeasonRacePoints> SeasonRacePoints { get; set; }

    public virtual DbSet<SeasonSprintPoints> SeasonSprintPoints { get; set; }

    public virtual DbSet<SocialMedia> SocialMedia { get; set; }

    public virtual DbSet<Sprint> Sprints { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=JKRSTANOVIC-W10;Database=F1statsv2;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Croatian_CI_AS");

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CountryId");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DriverId");

            entity.HasOne(d => d.SocialMedia).WithMany(p => p.Drivers)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Driver_SocialMediaId");

            entity.HasOne(d => d.Country).WithMany(p => p.Drivers)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Driver_CountryId");
        });

        modelBuilder.Entity<GrandPrix>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_GrandPrixId");

            entity.HasOne(d => d.Season).WithMany(p => p.GrandPrixes).HasConstraintName("FK_GrandPrix_SeasonId");

            entity.HasOne(d => d.Track).WithMany(p => p.GrandPrixes)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_GrandPrix_TrackId");

        });

        modelBuilder.Entity<League>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_LeagueId");

            entity.HasOne(d => d.SocialMedia).WithMany(p => p.Leagues)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_League_SocialMediaId");
        });

        modelBuilder.Entity<LeagueUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_LeagueUserId");

            entity.HasOne(d => d.League).WithMany(p => p.LeagueUsers).HasConstraintName("FK_LeagueUser_LeagueId");

            entity.HasOne(d => d.User).WithMany(p => p.LeagueUsers).HasConstraintName("FK_LeagueUser_UserId");
        });

        modelBuilder.Entity<Qualifying>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_QualifyingId");

            entity.HasOne(d => d.Driver).WithMany(p => p.Qualifyings).HasConstraintName("FK_Qualifying_DriverId");

            entity.HasOne(d => d.GrandPrix).WithMany(p => p.Qualifyings).HasConstraintName("FK_Qualifying_GrandPrixId");

            entity.HasOne(d => d.Team).WithMany(p => p.Qualifyings).HasConstraintName("FK_Qualifying_TeamId");
        });

        modelBuilder.Entity<Race>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_RaceId");

            entity.HasOne(d => d.Driver).WithMany(p => p.Races).HasConstraintName("FK_Race_DriverId");

            entity.HasOne(d => d.GrandPrix).WithMany(p => p.Races).HasConstraintName("FK_Race_GrandPrixId");

            entity.HasOne(d => d.Team).WithMany(p => p.Races).HasConstraintName("FK_Race_TeamId");
        });

        modelBuilder.Entity<Season>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SeasonId");

            entity.Property(e => e.LapsRequiredPercentage).HasDefaultValueSql("('90')");

            entity.HasOne(d => d.League).WithMany(p => p.Seasons).HasConstraintName("FK_Season_LeagueId");
        });

        modelBuilder.Entity<SeasonAssists>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SeasonAssists_SeasonId");

            entity.HasOne(d => d.Season).WithOne(p => p.SeasonAssist).HasConstraintName("FK_SeasonAssists_SeasonId");
        });

        modelBuilder.Entity<SeasonDrivers>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SeasonDriverId");

            entity.HasOne(d => d.Driver).WithMany(p => p.SeasonDrivers).HasConstraintName("FK_SeasonDriver_DriverId");

            entity.HasOne(d => d.Season).WithMany(p => p.SeasonDrivers).HasConstraintName("FK_SeasonDriver_SeasonId");

            entity.HasOne(d => d.Team).WithMany(p => p.SeasonDrivers).HasConstraintName("FK_SeasonDriver_TeamId");
        });

        modelBuilder.Entity<SeasonFastestLapPoints>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SeasonFastestLapPoints_SeasonId");

            entity.Property(e => e.Position).HasDefaultValueSql("('10')");
            entity.Property(e => e.Points).HasDefaultValueSql("('1')");

            entity.HasOne(d => d.Season).WithOne(p => p.SeasonFastestLapPoint).HasConstraintName("FK_SeasonFastestLapPoints_SeasonId");
        });

        modelBuilder.Entity<SeasonLobbySettings>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SeasonLobbySettings_SeasonId");

            entity.HasOne(d => d.Season).WithOne(p => p.SeasonLobbySetting).HasConstraintName("FK_SeasonLobbySettings_SeasonId");
        });

        modelBuilder.Entity<SeasonQualPoints>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SeasonQualPointsId");

            entity.HasOne(d => d.Season).WithMany(p => p.SeasonQualPoints).HasConstraintName("FK_SeasonQualPoints_SeasonId");
        });

        modelBuilder.Entity<SeasonRacePoints>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SeasonRacePointsId");

            entity.HasOne(d => d.Season).WithMany(p => p.SeasonRacePoints).HasConstraintName("FK_SeasonRacePoints_SeasonId");
        });

        modelBuilder.Entity<SeasonSprintPoints>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SeasonSprintPointsId");

            entity.HasOne(d => d.Season).WithMany(p => p.SeasonSprintPoints).HasConstraintName("FK_SeasonSprintPoints_SeasonId");
        });

        modelBuilder.Entity<SocialMedia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SocialMediaId");
        });

        modelBuilder.Entity<Sprint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SprintId");

            entity.HasOne(d => d.Driver).WithMany(p => p.Sprints).HasConstraintName("FK_SprintDriver_DriverId");

            entity.HasOne(d => d.GrandPrix).WithMany(p => p.Sprints).HasConstraintName("FK_SprintDriver_GrandPrixId");

            entity.HasOne(d => d.Team).WithMany(p => p.Sprints).HasConstraintName("FK_Sprint_TeamId");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TeamId");

            entity.Property(e => e.ColorHex).HasDefaultValueSql("('#000')");
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TrackId");

            entity.Property(e => e.Laps).HasDefaultValueSql("('52')");

            entity.HasOne(e => e.Country).WithMany(p => p.Tracks)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Track_CountryId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
