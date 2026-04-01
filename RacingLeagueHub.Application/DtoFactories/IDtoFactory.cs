using RacingLeagueHub.Domain.Entities;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.DtoFactories;

public interface IDtoFactory<TEntity, TDto>
    where TEntity : IEntity
{
    Expression<Func<TEntity, TDto>> ToDtoExpression();

    bool FromDto(TEntity entity, TDto dto);

    TDto ToDto(TEntity entity);
}
