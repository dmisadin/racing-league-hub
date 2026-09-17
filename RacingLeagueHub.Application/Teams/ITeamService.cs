using RacingLeagueHub.Application.Teams.Dtos;
using RacingLeagueHub.Domain.Common.Models;

namespace RacingLeagueHub.Application.Teams;

public interface ITeamService
{
    Task<TeamDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<PagedResult<TeamDto>> GetPagedAsync(int page, CancellationToken ct);
    Task<TeamDto> AddAsync(CreateTeamDto dto, CancellationToken ct);
    Task<TeamDto?> UpdateAsync(int id, UpdateTeamDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}