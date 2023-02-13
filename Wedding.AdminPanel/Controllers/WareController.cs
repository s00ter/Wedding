using Microsoft.AspNetCore.Mvc;
using Wedding.AdminPanel.Controllers.Abstractions;
using Wedding.AdminPanel.Models.Ware;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Repository.Abstractions;

namespace Wedding.AdminPanel.Controllers
{
    public class WareController : ReadWriteControllerBase<Ware, Guid, IWareRepository>
    {
        public WareController(IWareRepository repository) : base(repository)
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
        public async Task<IActionResult> Create([FromBody] CreateWareDto body)
        {
            var ware = new Ware
            {
                Name = body.Name,
                RetailPrice = body.RetailPrice,
                Price = body.Price,
                Discounted = body.Discounted,
                Description = body.Description
            };

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
