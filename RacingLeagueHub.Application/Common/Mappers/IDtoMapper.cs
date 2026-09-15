using RacingLeagueHub.Domain.Common.Entitites;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.Common.Mappers;

public interface IDtoMapper<TEntity, TDto>
    where TEntity : IEntity
{
    Expression<Func<TEntity, TDto>> ToDtoExpression();

    bool FromDto(TEntity entity, TDto dto);

    TDto ToDto(TEntity entity);
}
