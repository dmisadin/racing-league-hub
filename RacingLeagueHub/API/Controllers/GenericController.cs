using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Application.DtoFactories;
using RacingLeagueHub.Application.Dtos;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.BLL.Entities;
using RacingLeagueHub.BLL.Infrastructure;

namespace RacingLeagueHub.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class GenericController<TEntity, TDto> : Controller
    where TEntity : IEntity
    where TDto : BaseDto
{
    protected readonly IRepository<TEntity> repository;

    public GenericController(IRepository<TEntity> genericRepository)
    {
        this.repository = genericRepository;
    }

    protected abstract IDtoFactory<TEntity, TDto> DtoFactory { get; }

    [HttpGet("get-by-id/{id}")]
    public virtual async Task<ActionResult<TDto>> GetOne([FromRoute] EncryptedId id)
    {
        var dto = await repository.GetByIdAsync<TDto>(id.RawId, DtoFactory.ToDtoExpression());

        if (dto == null)
            return NotFound();

        return Ok(dto);
    }

    [HttpPost("add")]
    public virtual async Task<ActionResult<EncryptedId>> Add([FromBody] TDto dto)
    {
        var entity = repository.Create();
        DtoFactory.FromDto(entity, dto);

        await this.repository.InsertAsync(entity);
        await this.repository.CommitAsync();

        return Ok(new EncryptedId(entity.Id));
    }

    [HttpPost("update")]
    public virtual async Task<ActionResult<EncryptedId>> Update([FromBody] TDto dto)
    {
        var id = dto.Id?.RawId;
        if (id == null || id == 0)
            return BadRequest("Invalid ID.");

        var entity = await this.repository.FindAsync(id);

        if (entity == null)
            return NotFound();

        DtoFactory.FromDto(entity, dto);

        await this.repository.CommitAsync();

        return Ok(new EncryptedId(entity.Id));
    }

    [HttpDelete("delete/{id}")]
    public virtual async Task<IActionResult> Delete([FromRoute] EncryptedId id)
    {
        var rows = await repository.ExecuteDeleteAsync(x => x.Id == id.RawId);

        return rows == 0 ? NotFound() : NoContent();
    }

    /* Move to Application layer
    [HttpPost("upsert-many")]
    public async Task UpsertMany([FromBody]IEnumerable<TDto> dtos)
    {
        var toUpdate = dtos.Where(x => x.Id?.RawId > 0).ToList();
        var toInsert = dtos.Where(x => x.Id?.RawId == 0).ToList();

        var ids = toUpdate.Select(x => x.Id?.RawId).ToList();

        var existing = await repository.Query()
            .Where(e => ids.Contains(e.Id))
            .ToListAsync();

        foreach (var dto in toUpdate)
        {
            var entity = existing.FirstOrDefault(e => e.Id == dto.Id?.RawId);
            if (entity != null)
                DtoFactory.FromDto(entity, dto);
        }

        var newEntities = new List<TEntity>();

        foreach (var dto in toInsert)
        {
            var newEntity = repository.Create();
            DtoFactory.FromDto(newEntity, dto);
            newEntities.Add(newEntity);
        }

        await repository.InsertAsync(newEntities.ToArray());

        await repository.CommitAsync();
    }
    */
}
