using Microsoft.EntityFrameworkCore;
using RacingLeagueHub.Application.Users.Dtos;
using RacingLeagueHub.Application.Users.Persistence;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.Persistence.Users;

internal class UserQueries : IUserQueries
{
    private readonly RacingContext context;

    public UserQueries(RacingContext context) 
    {
        this.context = context;
    }

    public async Task<bool> IsEmailTakenAsync(string email, CancellationToken ct = default)
    {
        return await context.User.AnyAsync(u => u.Email == email, ct);
    }

    public async Task<bool> IsUsernameTakenAsync(string username, CancellationToken ct = default)
    {
        return await context.User.AnyAsync(u => u.Username == username, ct);
    }

    public async Task<User?> GetUserAsync(string email, CancellationToken ct = default)
    {
        return await context.User
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<User?> GetUserAsync(long id, CancellationToken ct = default)
    {
        return await context.User
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<UserDto?> GetByIdAsync(long userId, CancellationToken ct)
    {
        return await context.User
            .Where(u => u.Id == userId)
            .Select(u => new UserDto(u.Id, u.Email, u.Username, u.IsAdmin, u.DriverId))
            .FirstOrDefaultAsync(ct);
    }
}
