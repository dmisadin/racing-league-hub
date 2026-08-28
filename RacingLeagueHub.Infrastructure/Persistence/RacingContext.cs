using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Entities.Authentication;
using RacingLeagueHub.Domain.Entities.GrandsPrix;
using RacingLeagueHub.Domain.Entities.Resources;
using RacingLeagueHub.Domain.Entities.Seasons;
using RacingLeagueHub.Domain.Entities.Stewarding;

namespace RacingLeagueHub.Infrastructure.Persistence;

internal class RacingContext : DbContext
{
    public DbSet<User> User => Set<User>();

    public DbSet<League> League => Set<League>();

    public DbSet<LeagueUser> LeagueUser => Set<LeagueUser>();

    public DbSet<Season> Season => Set<Season>();

    public DbSet<SeasonAssists> SeasonAssists => Set<SeasonAssists>();

    public DbSet<SeasonDriver> SeasonDriver => Set<SeasonDriver>();

    public DbSet<SeasonLobbySettings> SeasonLobbySettings => Set<SeasonLobbySettings>();

    public DbSet<SeasonPoints> SeasonPoints => Set<SeasonPoints>();

    public DbSet<GrandPrix> GrandPrix => Set<GrandPrix>();

    public DbSet<GrandPrixDriver> GrandPrixDriver => Set<GrandPrixDriver>();

    public DbSet<GrandPrixResult> GrandPrixResult => Set<GrandPrixResult>();

    public DbSet<Driver> Driver => Set<Driver>();

    public DbSet<Team> Team => Set<Team>();

    public DbSet<GameTeam> GameTeam => Set<GameTeam>();

    public DbSet<Track> Track => Set<Track>();

    public DbSet<TrackLayout> TrackLayout => Set<TrackLayout>();

    public DbSet<TrackLayoutGame> TrackLayoutGame => Set<TrackLayoutGame>();

    public DbSet<Country> Country => Set<Country>();

    public DbSet<Incident> Incident => Set<Incident>();

    public DbSet<Verdict> Verdict => Set<Verdict>();

    public DbSet<RefreshToken> RefreshToken => Set<RefreshToken>();

    public DbSet<UserExternalLogin> UserExternalLogin => Set<UserExternalLogin>();

    public DbSet<UserRecoveryCode> UserRecoveryCode => Set<UserRecoveryCode>();

    public DbSet<PasswordResetToken> PasswordResetToken => Set<PasswordResetToken>();

    public DbSet<Resource> Resource => Set<Resource>();

    public RacingContext(DbContextOptions<RacingContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyDbMapsFromAssembly(typeof(RacingContext).Assembly);
    }
}
