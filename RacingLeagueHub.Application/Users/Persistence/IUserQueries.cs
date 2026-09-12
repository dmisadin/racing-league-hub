using RacingLeagueHub.Application.Users.Dtos;
using RacingLeagueHub.Application.Users.Models;
using RacingLeagueHub.Domain.Entities;

namespace RacingLeagueHub.Application.Users.Persistence;

public interface IUserQueries
{
    Task<bool> IsEmailTakenAsync(string email, CancellationToken ct = default);
    Task<bool> IsUsernameTakenAsync(string username, CancellationToken ct = default);
    Task<User?> GetUserAsync(string email, CancellationToken ct = default);
    Task<User?> GetUserAsync(long id, CancellationToken ct = default);
    Task<UserDto?> GetByIdAsync(long userId, CancellationToken ct);
}