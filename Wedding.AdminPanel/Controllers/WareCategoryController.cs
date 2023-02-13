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
        public async Task<IActionResult> Create([FromBody] CreateWareCategoryDto body)
        {
            var wareCategory = new WareCategory
            {
                Name = body.Name
            };

            await Repository.Create(wareCategory);

            return NoContent();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var wareCategory = await Repository.GetByIdAsync(id);
            await Repository.Delete(wareCategory);

            return RedirectToAction(nameof(Index));
        }
    }
}
