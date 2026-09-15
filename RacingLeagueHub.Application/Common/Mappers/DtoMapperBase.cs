using RacingLeagueHub.Application.Common.Dtos;
using RacingLeagueHub.Domain.Common.Entitites;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.Common.Mappers;

public abstract class DtoMapperBase<TEntity, TDto> : IDtoMapper<TEntity, TDto>
    where TEntity : IEntity
    where TDto : BaseDto
{
    private Func<TEntity, TDto> ToDtoCompiled;

    public abstract bool FromDto(TEntity entity, TDto dto);
    public abstract Expression<Func<TEntity, TDto>> ToDtoExpression();

    public TDto ToDto(TEntity entity)
    {
        ToDtoCompiled ??= ToDtoExpression().Compile();
        return ToDtoCompiled(entity);
    }
}
