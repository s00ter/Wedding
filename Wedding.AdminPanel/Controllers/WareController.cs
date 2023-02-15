using Microsoft.AspNetCore.Mvc;
using Wedding.AdminPanel.Controllers.Abstractions;
using Wedding.AdminPanel.Models.Ware;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Repository.Abstractions;

namespace Wedding.AdminPanel.Controllers
{
    public class WareController : ReadWriteControllerBase<Ware, Guid, IWareRepository>
    {
        private readonly IWareCategoryRepository _wareCategoryRepository;

        public WareController(IWareRepository repository, IWareCategoryRepository wareCategoryRepository) : base(repository)
        {
            _wareCategoryRepository = wareCategoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var items = await Repository.GetAllAsync();

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _wareCategoryRepository.GetAllAsync();

            ViewBag.Categories = categories;

            return PartialView();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateWareDto body)
        {
            var ware = new Ware
            {
                Name = body.Name,
                RetailPrice = body.RetailPrice,
                Price = body.Price,
                Discounted = body.Discounted,
                Description = body.Description,
                CategoryId = body.CategoryId
            };

            if (body.File != null)
            {
                using var dataStream = new MemoryStream();
                await body.File.CopyToAsync(dataStream);
                var imageBytes = dataStream.ToArray();

                ware.FileBytes = imageBytes;
            }

            await Repository.Create(ware);

            return NoContent();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var ware = await Repository.GetByIdAsync(id);
            await Repository.Delete(ware);

            return RedirectToAction(nameof(Index));
        }

    }
}
