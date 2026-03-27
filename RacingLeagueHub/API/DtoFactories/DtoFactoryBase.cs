using RacingLeagueHub.BLL.Entities;
using System.Linq.Expressions;

namespace RacingLeagueHub.API.DtoFactories;

public abstract class DtoFactoryBase<TEntity, TDto> : IDtoFactory<TEntity, TDto>
    where TEntity : IEntity
{
    private Func<TEntity, TDto> ToDtoCompiled;

    public abstract void FromDto(TEntity entity, TDto dto);
    public abstract Expression<Func<TEntity, TDto>> ToDtoExpression();

    public TDto ToDto(TEntity entity)
    {
        ToDtoCompiled ??= ToDtoExpression().Compile();
        return ToDtoCompiled(entity);
    }
}
