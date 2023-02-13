using Microsoft.AspNetCore.Mvc;
using Wedding.AdminPanel.Controllers.Abstractions;
using Wedding.AdminPanel.Models.Salon;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Repository.Abstractions;

namespace Wedding.AdminPanel.Controllers
{
    public class SalonController : ReadWriteControllerBase<Salon, int, ISalonRepository>
    {
        public SalonController(ISalonRepository repository) : base(repository)
        {
        }

        public async Task<IActionResult> Index()
        {
            var items = await Repository.GetAllAsync();

            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSalonDto body)
        {
            var salon = new Salon
            {
                Address = body.Address
            };

            await Repository.Create(salon);

            return NoContent();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var city = await Repository.GetByIdAsync(id);
            await Repository.Delete(city);

            return RedirectToAction(nameof(Index));
        }
    }
}
