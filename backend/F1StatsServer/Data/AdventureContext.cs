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

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GrandPrix> GrandPrixes { get; set; }

    public virtual DbSet<League> Leagues { get; set; }

    public virtual DbSet<LeagueUser> LeagueUsers { get; set; }

    public virtual DbSet<Platform> Platforms { get; set; }

    public virtual DbSet<Qualifying> Qualifyings { get; set; }

    public virtual DbSet<Race> Races { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Season> Seasons { get; set; }

    public virtual DbSet<SeasonAssist> SeasonAssists { get; set; }

    public virtual DbSet<SeasonDriver> SeasonDrivers { get; set; }

    public virtual DbSet<SeasonFastestLapPoint> SeasonFastestLapPoints { get; set; }

    public virtual DbSet<SeasonLobbySetting> SeasonLobbySettings { get; set; }

    public virtual DbSet<SeasonQualPoint> SeasonQualPoints { get; set; }

    public virtual DbSet<SeasonRacePoint> SeasonRacePoints { get; set; }

    public virtual DbSet<SeasonSprintPoint> SeasonSprintPoints { get; set; }

    public virtual DbSet<SocialMedium> SocialMedia { get; set; }

    public virtual DbSet<Sprint> Sprints { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=JKRSTANOVIC-W10;Database=F1statsv2;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Croatian_100_CI_AS");

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Country__3214EC07A75AB5FA");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Driver__3214EC07957B33AC");

            entity.HasOne(d => d.Platform).WithMany(p => p.Drivers)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Driver_PlatformId");

            entity.HasOne(d => d.SocialMedia).WithMany(p => p.Drivers)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Driver_SocialMediaId");

            entity.HasOne(d => d.Country).WithMany(p => p.Drivers)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Driver_CountryId");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Game__3214EC0764E480FF");
        });

        modelBuilder.Entity<GrandPrix>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GrandPri__3214EC0760201A28");

            entity.HasOne(d => d.Season).WithMany(p => p.GrandPrixes).HasConstraintName("FK_GrandPrix_SeasonId");

            entity.HasOne(d => d.Track).WithMany(p => p.GrandPrixes)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_GrandPrix_TrackId");

            entity.HasOne(d => d.Country).WithMany(p => p.GrandPrixes)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_GrandPrix_CountryId");

        });

        modelBuilder.Entity<League>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__League__3214EC07379E033B");

            entity.HasOne(d => d.Region).WithMany(p => p.Leagues).HasConstraintName("FK_League_RegionId");

            entity.HasOne(d => d.SocialMedia).WithMany(p => p.Leagues)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_League_SocialMediaId");
        });

        modelBuilder.Entity<LeagueUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LeagueUs__3214EC076AB8F897");

            entity.HasOne(d => d.League).WithMany(p => p.LeagueUsers).HasConstraintName("FK_LeagueUser_LeagueId");

            entity.HasOne(d => d.User).WithMany(p => p.LeagueUsers).HasConstraintName("FK_LeagueUser_UserId");
        });

        modelBuilder.Entity<Platform>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platform__3214EC07500B5EF5");
        });

        modelBuilder.Entity<Qualifying>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Qualifyi__3214EC074E099639");

            entity.HasOne(d => d.Driver).WithMany(p => p.Qualifyings).HasConstraintName("FK_Qualifying_DriverId");

            entity.HasOne(d => d.GrandPrix).WithMany(p => p.Qualifyings).HasConstraintName("FK_Qualifying_GrandPrixId");

            entity.HasOne(d => d.Team).WithMany(p => p.Qualifyings).HasConstraintName("FK_Qualifying_TeamId");
        });

        modelBuilder.Entity<Race>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Race__3214EC0750D3589B");

            entity.HasOne(d => d.Driver).WithMany(p => p.Races).HasConstraintName("FK_Race_DriverId");

            entity.HasOne(d => d.GrandPrix).WithMany(p => p.Races).HasConstraintName("FK_Race_GrandPrixId");

            entity.HasOne(d => d.Team).WithMany(p => p.Races).HasConstraintName("FK_Race_TeamId");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Region__3214EC07AA3ED67F");
        });

        modelBuilder.Entity<Season>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Season__3214EC07AE88DF26");

            entity.Property(e => e.LapsRequiredPercentage).HasDefaultValueSql("('90')");

            entity.HasOne(d => d.GameNavigation).WithMany(p => p.Seasons).HasConstraintName("FK_Season_GameId");

            entity.HasOne(d => d.League).WithMany(p => p.Seasons).HasConstraintName("FK_Season_LeagueId");

            entity.HasOne(d => d.Platform).WithMany(p => p.Seasons)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Season_PlatformId");
        });

        modelBuilder.Entity<SeasonAssist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SeasonAs__3214EC0773A7061B");

            entity.HasOne(d => d.Season).WithMany(p => p.SeasonAssists).HasConstraintName("FK_SeasonAssists_SeasonId");
        });

        modelBuilder.Entity<SeasonDriver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SeasonDr__3214EC07601D5C59");

            entity.HasOne(d => d.Driver).WithMany(p => p.SeasonDrivers).HasConstraintName("FK_SeasonDriver_DriverId");

            entity.HasOne(d => d.Season).WithMany(p => p.SeasonDrivers).HasConstraintName("FK_SeasonDriver_SeasonId");

            entity.HasOne(d => d.Team).WithMany(p => p.SeasonDrivers).HasConstraintName("FK_SeasonDriver_TeamId");
        });

        modelBuilder.Entity<SeasonFastestLapPoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SeasonFa__3214EC07AC8337D3");

            entity.Property(e => e.FinishInsideTopN).HasDefaultValueSql("('10')");
            entity.Property(e => e.Points).HasDefaultValueSql("('1')");

            entity.HasOne(d => d.Season).WithMany(p => p.SeasonFastestLapPoints).HasConstraintName("FK_SeasonFastestLapPoints_SeasonId");
        });

        modelBuilder.Entity<SeasonLobbySetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SeasonLo__3214EC070543EA8F");

            entity.HasOne(d => d.Season).WithMany(p => p.SeasonLobbySettings).HasConstraintName("FK_SeasonLobbySettings_SeasonId");
        });

        modelBuilder.Entity<SeasonQualPoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SeasonQu__3214EC079B192CD5");

            entity.HasOne(d => d.Season).WithMany(p => p.SeasonQualPoints).HasConstraintName("FK_SeasonQualPoints_SeasonId");
        });

        modelBuilder.Entity<SeasonRacePoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SeasonRa__3214EC0729EA724F");

            entity.HasOne(d => d.Season).WithMany(p => p.SeasonRacePoints).HasConstraintName("FK_SeasonRacePoints_SeasonId");
        });

        modelBuilder.Entity<SeasonSprintPoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SeasonSp__3214EC070915FC3D");

            entity.HasOne(d => d.Season).WithMany(p => p.SeasonSprintPoints).HasConstraintName("FK_SeasonSprintPoints_SeasonId");
        });

        modelBuilder.Entity<SocialMedium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SocialMe__3214EC07B022C64D");
        });

        modelBuilder.Entity<Sprint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sprint__3214EC07F7CEC330");

            entity.HasOne(d => d.Driver).WithMany(p => p.Sprints).HasConstraintName("FK_SprintDriver_DriverId");

            entity.HasOne(d => d.GrandPrix).WithMany(p => p.Sprints).HasConstraintName("FK_SprintDriver_GrandPrixId");

            entity.HasOne(d => d.Team).WithMany(p => p.Sprints).HasConstraintName("FK_Sprint_TeamId");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Team__3214EC0747A43CBA");

            entity.Property(e => e.ColorHex).HasDefaultValueSql("('#000')");
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Track__3214EC078A666317");

            entity.Property(e => e.Laps).HasDefaultValueSql("('52')");

            entity.HasOne(e => e.Country).WithMany(p => p.Tracks)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Track_CountryId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07F7F96EAB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
