using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Services.GameTeamService.Dtos;
using RacingLeagueHub.Application.Services.GameTeamService.Persistence;

namespace RacingLeagueHub.Application.Services.GameTeamService;

public class GameTeamService : IGameTeamService
{
    private readonly IGameTeamQueries queries;
    private readonly IGameTeamCommands commands;

    public GameTeamService(
        IGameTeamQueries queries,
        IGameTeamCommands commands)
    {
        this.queries = queries;
        this.commands = commands;
    }

    public async Task<GameTeamDto?> GetByIdAsync(long id, CancellationToken ct)
    {
        return await queries.GetByIdAsync(id, ct);
    }

    public async Task<PagedResult<GameTeamDto>> GetPagedAsync(int page, CancellationToken ct)
    {
        return await queries.GetPagedAsync(page, pageSize: 10, ct);
    }

    public async Task<GameTeamDto> AddAsync(CreateGameTeamDto dto, CancellationToken ct)
    {
        return await commands.AddAsync(dto, ct);
    }

    public async Task<GameTeamDto?> UpdateAsync(long id, UpdateGameTeamDto dto, CancellationToken ct)
    {
        return await commands.UpdateAsync(id, dto, ct);
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken ct)
    {
        return await commands.DeleteAsync(id, ct);
    }
}
