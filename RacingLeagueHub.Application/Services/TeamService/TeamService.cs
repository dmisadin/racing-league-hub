using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Services.TeamService.Dtos;
using RacingLeagueHub.Application.Services.TeamService.Persistence;

namespace RacingLeagueHub.Application.Services.TeamService;

public class TeamService : ITeamService
{
    private readonly ITeamQueries queries;
    private readonly ITeamCommands commands;

    public TeamService(
        ITeamQueries queries,
        ITeamCommands commands)
    {
        this.queries = queries;
        this.commands = commands;
    }

    public async Task<TeamDto?> GetByIdAsync(long id, CancellationToken ct)
    {
        return await queries.GetByIdAsync(id, ct);
    }

    public async Task<PagedResult<TeamDto>> GetPagedAsync(int page, CancellationToken ct)
    {
        return await queries.GetPagedAsync(page, pageSize: 10, ct);
    }

    public async Task<TeamDto> AddAsync(CreateTeamDto dto, CancellationToken ct)
    {
        return await commands.AddAsync(dto, ct);
    }

    public async Task<TeamDto?> UpdateAsync(long id, UpdateTeamDto dto, CancellationToken ct)
    {
        return await commands.UpdateAsync(id, dto, ct);
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken ct)
    {
        return await commands.DeleteAsync(id, ct);
    }
}