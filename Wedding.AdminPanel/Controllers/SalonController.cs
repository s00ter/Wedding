using Microsoft.AspNetCore.Mvc;
using Wedding.AdminPanel.Controllers.Abstractions;
using Wedding.AdminPanel.Models.Salon;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Repository.Abstractions;

namespace Wedding.AdminPanel.Controllers
{
    public class SalonController : ReadWriteControllerBase<Salon, int, ISalonRepository>
    {
        private readonly ICityRepository _cityRepository;
        public SalonController(ISalonRepository repository,ICityRepository cityRepository) : base(repository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<IActionResult> Index()
        {
            var items = await Repository.GetAllAsync();

            return View(items);
        }

        [HttpGet]
        public async Task <IActionResult> Create()
        {
            var city = await _cityRepository.GetAllAsync();

            ViewBag.City = city;

            return PartialView();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateSalonDto body)
        {
            var salon = new Salon
            {
                Address = body.Address,
                CityId = body.CityId
            };

            if (body.File != null)
            {
                using var dataStream = new MemoryStream();
                await body.File.CopyToAsync(dataStream);
                var imageBytes = dataStream.ToArray();

                salon.FileBytes = imageBytes;
            }

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
