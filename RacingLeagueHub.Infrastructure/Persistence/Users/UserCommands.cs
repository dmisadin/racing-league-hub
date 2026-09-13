using RacingLeagueHub.Application.Users.Dtos;
using RacingLeagueHub.Application.Users.Persistence;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Infrastructure.Persistence.Users;

internal class UserCommands : IUserCommands
{
    private readonly RacingContext context;

    public UserCommands(RacingContext context)
    {
        this.context = context;
    }

    public async Task<UserDto> AddAsync(User user, CancellationToken ct = default)
    {
        await context.User.AddAsync(user);

        await context.SaveChangesAsync(ct);

        return new UserDto(user.Id, user.Email, user.Username, user.IsAdmin, user.DriverId);
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
    {
        return context.SaveChangesAsync(ct);
    }
}
