using Microsoft.AspNetCore.Mvc;
using Test.Domain.Entities;
using Test.Domain.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarBrandsController : ControllerBase
    {
        private readonly IGenericService<CarBrands, Guid> _carBrandsService;

        public CarBrandsController(IGenericService<CarBrands, Guid> carBrandsService)
        {
            _carBrandsService = carBrandsService;
        }
        // GET: api/<CarBrandsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _carBrandsService.GetAllAsync());
        }

        // GET api/<CarBrandsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _carBrandsService.GetAsync(id));
        }

        // POST api/<CarBrandsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CarBrands carBrands)
        {
            return Ok(await _carBrandsService.AddAsync(carBrands));
        }

        // PUT api/<CarBrandsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] string brand)
        {
            return Ok(await _carBrandsService.UpdateAsync(new CarBrands() { Id = id, Brand = brand}));
        }

        // DELETE api/<CarBrandsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _carBrandsService.DeleteAsync(id));
        }
    }
}
