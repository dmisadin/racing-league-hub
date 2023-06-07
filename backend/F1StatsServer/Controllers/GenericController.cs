using F1StatsServer.Interface;
using Microsoft.AspNetCore.Mvc;

namespace F1StatsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T> : Controller where T : class
    {
        private readonly IGenericRepository<T> _genericRepository;

        public GenericController(IGenericRepository<T> genericRepository)
        {
            _genericRepository = genericRepository;
        }

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

            return Ok(generic);
        }

        //[HttpDelete]
        //public IActionResult DeleteItem(int id)
        //{
        //    var generic = _genericRepository.DeleteItem(id);
        //    return Ok(generic);
        //}

        //[HttpPost]
        //public IActionResult CreateItem(T item)
        //{
        //    var generic = _genericRepository.CreateItem(item);
        //    return Ok(generic);
        //}
    }
}
