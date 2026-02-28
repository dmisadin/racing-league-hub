using RacingLeagueHub.BLL.Mapping.DtoFactories;
using RacingLeagueHub.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RacingLeagueHub.Infrastructure;

[Route("api/[controller]")]
[ApiController]
public abstract class GenericController<TEntity, TDto> : Controller where TEntity : EntityBase
{
    private readonly IRepository<TEntity> genericRepository;

    public GenericController(IRepository<TEntity> genericRepository)
    {
        this.genericRepository = genericRepository;
    }

    protected abstract IDtoFactory<TEntity, TDto> DtoFactory { get; }

    [HttpGet("get-by-id")]
    public virtual async Task<ActionResult<TDto>> GetOne([FromQuery] long id)
    {
        var entity = await genericRepository.FindAsync(id);

        if (entity == null)
            return NotFound();

        var dto = DtoFactory.ToDto(entity);

        return Ok(dto);
    }

    [HttpPost("add")]
    public virtual async Task<ActionResult<long>> Add(TDto dto)
    {
        var entity = genericRepository.Create();
        DtoFactory.FromDto(entity, dto);

        await this.genericRepository.InsertAsync(entity);
        await this.genericRepository.CommitAsync();

        return Ok(entity.Id);
    }

    [HttpPost("update")]
    public virtual async Task<ActionResult<long>> Update([FromBody] TDto dto, [FromQuery] long id)
    {
        var entity = this.genericRepository.FindAsync(id).Result;

        if (entity == null)
            return NotFound();

        DtoFactory.FromDto(entity, dto);

        return Ok(entity.Id);
    }

    [HttpDelete("delete")]
    public virtual async Task<IActionResult> Delete(long id)
    {
        var rows = await genericRepository.Query()
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return rows == 0 ? NotFound() : NoContent();
    }
}
