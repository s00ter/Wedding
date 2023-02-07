using Microsoft.AspNetCore.Mvc;
using Wedding.DAL.Repository.Abstractions;

namespace Wedding.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;

        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var items = await _cityRepository.GetAllAsync();

            return Ok(items);
        }

    }
}
