using RacingLeagueHub.Domain.Users;

namespace RacingLeagueHub.Application.Identity.Authentication.ExternalLogins.Persistence;

public interface IUserExternalLoginCommands
{
    Task AddAsync(UserExternalLogin externalLogin, CancellationToken ct = default);
}
