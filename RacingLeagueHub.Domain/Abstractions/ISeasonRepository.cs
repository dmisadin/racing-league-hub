using RacingLeagueHub.Domain.Entities.Seasons;
using RacingLeagueHub.Domain.Infrastructure;
using System.Linq.Expressions;

namespace RacingLeagueHub.Domain.Abstractions;

public interface ISeasonRepository : IRepository<Season>
{
    Task<TDto?> GetBySlugAsync<TDto>(string leagueSlug, string seasonSlug, Expression<Func<Season, TDto>> selector, CancellationToken ct = default);
    Task<IList<TDto>?> GetLeagueSeasonsAsync<TDto>(string leagueSlug, Expression<Func<Season, TDto>> selector, CancellationToken ct = default);
}
