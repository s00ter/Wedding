using Microsoft.AspNetCore.Mvc;
using Wedding.AdminPanel.Controllers.Abstractions;
using Wedding.AdminPanel.Models.City;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Repository.Abstractions;

namespace Wedding.AdminPanel.Controllers
{
    public class CityController : ReadWriteControllerBase<City, int, ICityRepository>
    {
        public CityController(ICityRepository repository) : base(repository)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[controller]/Table")]
        public async Task<IActionResult> Table()
        {
            var items = await Repository.GetAllAsync();

            return PartialView(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCityDto body)
        {
            var city = new City
            {
                Name = body.Name
            };

            await Repository.Create(city);

            return NoContent();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var city = await Repository.GetByIdAsync(id);
            await Repository.Delete(city);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var item = await Repository.GetByIdAsync(id);

            return PartialView(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateCityDto body)
        {
            var item = await Repository.GetByIdAsync(body.Id);

            item.Name = body.Name;

            await Repository.Update(item);

            return NoContent();
        }
    }
}
