using RacingLeagueHub.Application.Users.Dtos;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Application.Users.Persistence;

public interface IUserCommands
{
    Task<UserDto> AddAsync(User entity, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
