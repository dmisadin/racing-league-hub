using RacingLeagueHub.Domain.Entities;
using RacingLeagueHub.Domain.Infrastructure;
using System.Linq.Expressions;

namespace RacingLeagueHub.Domain.Abstractions;

public interface ILeagueRepository : IRepository<League>
{
    Task<TDto?> GetBySlugAsync<TDto>(string slug, Expression<Func<League, TDto>> selector);
}
