using RacingLeagueHub.BLL.Entities;
using System.Linq.Expressions;

namespace RacingLeagueHub.API.DtoFactories;

public interface IDtoFactory<TEntity, TDto>
    where TEntity : IEntity
{
    Expression<Func<TEntity, TDto>> ToDtoExpression();

    void FromDto(TEntity entity, TDto dto);

    TDto ToDto(TEntity entity);
}
