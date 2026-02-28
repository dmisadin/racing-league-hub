using F1StatsServer.Entities;
using System.Linq.Expressions;

namespace F1StatsServer.BLL.Mapping.DtoFactories;

public interface IDtoFactory<TEntity, TDto>
    where TEntity : IEntity
{
    Expression<Func<TEntity, TDto>> ToDtoExpression();

    void FromDto(TEntity entity, TDto dto);

    TDto ToDto(TEntity entity);
}
