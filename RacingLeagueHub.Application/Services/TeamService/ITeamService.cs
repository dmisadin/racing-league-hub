using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Services.TeamService.Dtos;

namespace RacingLeagueHub.Application.Services.TeamService;

public interface ITeamService
{
    Task<TeamDto?> GetByIdAsync(long id, CancellationToken ct);
    Task<PagedResult<TeamDto>> GetPagedAsync(int page, CancellationToken ct);
    Task<TeamDto> AddAsync(CreateTeamDto dto, CancellationToken ct);
    Task<TeamDto?> UpdateAsync(long id, UpdateTeamDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(long id, CancellationToken ct);
}