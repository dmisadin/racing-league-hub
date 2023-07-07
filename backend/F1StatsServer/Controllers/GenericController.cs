using AutoMapper;
using F1StatsServer.Interface;
using F1StatsServer.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F1StatsServer.Controllers
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

        [Authorize]
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Get()
        {
            var generic = _genericRepository.Get();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(generic);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (!_genericRepository.Has(id))
                return NotFound();

            var generic = _genericRepository.GetById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(generic);
        }

        [HttpDelete]
        public IActionResult DeleteItem(int id)
        {
            if (!_genericRepository.Has(id))
                return NotFound();

            var generic = _genericRepository.DeleteItem(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(generic);
        }

        [HttpPost]
        public IActionResult CreateItem(TDto item)
        {
            var itemFull = MyMapper<T, TDto>.Map(item);
            var generic = _genericRepository.CreateItem(itemFull);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(generic);
        }
    }
}
