using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Infrastructure;

namespace RacingLeagueHub.Domain.Abstractions;

public interface IUserRepository : IRepository<User>
{
    Task<bool> IsEmailTakenAsync(string email, CancellationToken ct = default);
    Task<bool> IsUsernameTakenAsync(string username, CancellationToken ct = default);
    Task<User?> FindByEmailAsync(string email, CancellationToken ct = default);
}
