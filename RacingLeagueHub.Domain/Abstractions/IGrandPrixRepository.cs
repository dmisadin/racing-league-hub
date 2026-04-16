using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Entities.GrandsPrix;
using RacingLeagueHub.Domain.Infrastructure;
using System.Linq.Expressions;

namespace RacingLeagueHub.Domain.Abstractions;

public interface IGrandPrixRepository : IRepository<GrandPrix>
{
    Task<TDto?> GetBySlugAsync<TDto>(string leagueSlug, string seasonSlug, string grandPrixSlug, Expression<Func<GrandPrix, TDto>> selector, CancellationToken ct = default);
    Task<PagedResult<TDto>?> GetSeasonGrandsPrixAsync<TDto>(string leagueSlug, string seasonSlug, Expression<Func<GrandPrix, TDto>> selector, int page = 1, int pageSize = 10, CancellationToken ct = default);
    Task<PagedResult<TDto>?> GetSeasonGrandsPrixAsync<TDto>(long seasonId, Expression<Func<GrandPrix, TDto>> selector, int page = 1, int pageSize = 10, CancellationToken ct = default);
}
