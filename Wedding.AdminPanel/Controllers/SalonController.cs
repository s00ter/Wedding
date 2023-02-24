using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wedding.AdminPanel.Constants;
using Wedding.AdminPanel.Controllers.Abstractions;
using Wedding.AdminPanel.Models.Pagination;
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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[controller]/Table")]
        public async Task<IActionResult> Table(int? page)
        {
            page ??= 1;
            var query = Repository.GetQuery();

            var total = await query
                .CountAsync();

            var items = await query
                .Skip((page.Value - 1) * PaginationConstants.ElementsOnPage)
                .Take(PaginationConstants.ElementsOnPage)
                .ToListAsync();

            var result = new BasePagedModel<Salon>
            {
                Total = total,
                Page = page.Value,
                PageItems = items
            };

            return PartialView(result);
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

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var cities = await _cityRepository.GetAllAsync();

            var item = await Repository.GetByIdAsync(id);

            ViewBag.City = cities;

            return PartialView(item);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] UpdateSalonDto body)
        {
            var item = await Repository.GetByIdAsync(body.Id);

            item.Address = body.Address;
            item.CityId = body.CityId;

            if (body.File != null)
            {
                using var dataStream = new MemoryStream();
                await body.File.CopyToAsync(dataStream);
                var imageBytes = dataStream.ToArray();

                item.FileBytes = imageBytes;
            }

            await Repository.Update(item);

            return NoContent();
        }
    }
}
