using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Domain.Abstractions;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.Repositories;

internal class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AdventureContext dbContext) : base(dbContext)
    {
    }

    public async Task<User?> FindByEmailAsync(string email, CancellationToken ct = default)
    {
        return await Query().FirstOrDefaultAsync(u => u.Email == email, ct);
    }

    public async Task<bool> IsEmailTakenAsync(string email, CancellationToken ct = default)
    {
        return await Query().AnyAsync(u => u.Email == email, ct);
    }

    public async Task<bool> IsUsernameTakenAsync(string username, CancellationToken ct = default)
    {
        return await Query().AnyAsync(u => u.Username == username, ct);
    }
}
