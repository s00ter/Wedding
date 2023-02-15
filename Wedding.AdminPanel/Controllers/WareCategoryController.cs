using Microsoft.AspNetCore.Mvc;
using Wedding.AdminPanel.Controllers.Abstractions;
using Wedding.AdminPanel.Models.WareCategory;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Repository.Abstractions;

namespace Wedding.AdminPanel.Controllers
{
    public class WareCategoryController : ReadWriteControllerBase<WareCategory, Guid, IWareCategoryRepository>
    {
        public WareCategoryController(IWareCategoryRepository repository) : base(repository)
        {
        }

        [HttpGet]
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
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateWareCategoryDto body)
        {
            var wareCategory = new WareCategory
            {
                Name = body.Name
            };

            if (body.File != null)
            {
                using var dataStream = new MemoryStream();
                await body.File.CopyToAsync(dataStream);
                var imageBytes = dataStream.ToArray();

                wareCategory.FileBytes = imageBytes;
            }

            await Repository.Create(wareCategory);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var wareCategory = await Repository.GetByIdAsync(id);
            await Repository.Delete(wareCategory);

            return RedirectToAction(nameof(Index));
        }

    }
}
