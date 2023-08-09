using AutoMapper;
using F1StatsServer.Interface;
using F1StatsServer.Model;
using F1StatsServer.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;

namespace F1StatsServer.Infrastructure
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T, TDto> : Controller where T : EntityBase
    {
        private readonly IGenericRepository<T> _genericRepository;

        public GenericController(IGenericRepository<T> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var generic = MyMapper<TDto, T>.MapList(await _genericRepository.GetAsync());


            return Ok(generic);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetById(int id)
        {
            var generic = await _genericRepository.GetByIdAsync(id);

            if (generic == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(generic);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var generic = await _genericRepository.DeleteItemAsync(id);

            if (generic == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(generic);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateItem(TDto item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemFull = MyMapper<T, TDto>.Map(item);
            var generic = await _genericRepository.CreateItemAsync(itemFull);

            return Ok(generic);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateItem([FromBody]JsonPatchDocument<T> item, int id)
        {
            foreach (var operation in item.Operations)
                if (operation.OperationType != Microsoft.AspNetCore.JsonPatch.Operations.OperationType.Replace)
                    return BadRequest("Patch request must contain only replace operations");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _genericRepository.UpdateItemAsync(item, id);

            return Ok(result);
        }
    }
}
