using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Application.Identity.Authentication.ExternalLogins.Persistence;

public interface IUserExternalLoginCommands
{
    Task AddAsync(UserExternalLogin externalLogin, CancellationToken ct = default);
}
