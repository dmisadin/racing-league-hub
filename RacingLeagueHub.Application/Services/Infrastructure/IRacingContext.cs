using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Entities.Authentication;
using RacingLeagueHub.Domain.Entities.GrandsPrix;
using RacingLeagueHub.Domain.Entities.Resources;
using RacingLeagueHub.Domain.Entities.Seasons;
using RacingLeagueHub.Domain.Entities.Stewarding;

namespace RacingLeagueHub.Application.Services.Infrastructure;

public interface IRacingContext
{
    DbSet<User> User { get; }

    DbSet<League> League { get; }
    DbSet<LeagueUser> LeagueUser { get; }

    DbSet<Season> Season { get; }
    DbSet<SeasonAssists> SeasonAssists { get; }
    DbSet<SeasonDriver> SeasonDriver { get; }
    DbSet<SeasonLobbySettings> SeasonLobbySettings { get; }
    DbSet<SeasonPoints> SeasonPoints { get; }

    DbSet<GrandPrix> GrandPrix { get; }
    DbSet<GrandPrixDriver> GrandPrixDriver { get; }
    DbSet<GrandPrixResult> GrandPrixResult { get; }

    DbSet<Driver> Driver { get; }
    DbSet<Team> Team { get; } // šifrarnik
    DbSet<GameTeam> GameTeam { get; }
    DbSet<Track> Track { get; } // šifrarnik
    DbSet<TrackLayout> TrackLayout { get; }
    DbSet<TrackLayoutGame> TrackLayoutGame { get; }
    DbSet<Country> Country { get; } // šifrarnik

    DbSet<Incident> Incident { get; }
    DbSet<Verdict> Verdict { get; }

    DbSet<RefreshToken> RefreshToken { get; }
    DbSet<UserExternalLogin> UserExternalLogin { get; }
    DbSet<UserRecoveryCode> UserRecoveryCode { get; }
    DbSet<PasswordResetToken> PasswordResetToken { get; }

    DbSet<Resource> Resource { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
