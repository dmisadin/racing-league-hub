using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Entities.Seasons;
using RacingLeagueHub.Domain.Infrastructure;
using System.Linq.Expressions;

namespace RacingLeagueHub.Domain.Abstractions;

public interface ISeasonRepository : IRepository<Season>
{
    Task<TDto?> GetBySlugAsync<TDto>(string leagueSlug, string seasonSlug, Expression<Func<Season, TDto>> selector, CancellationToken ct = default);
    Task<PagedResult<TDto>?> GetLeagueSeasonsAsync<TDto>(string leagueSlug, Expression<Func<Season, TDto>> selector, int page = 1, int pageSize = 10, CancellationToken ct = default);
    Task<PagedResult<TDto>?> GetLeagueSeasonsAsync<TDto>(long leagueId, Expression<Func<Season, TDto>> selector, int page = 1, int pageSize = 10, CancellationToken ct = default);
}
