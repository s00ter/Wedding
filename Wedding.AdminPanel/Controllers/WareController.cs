using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wedding.AdminPanel.Constants;
using Wedding.AdminPanel.Controllers.Abstractions;
using Wedding.AdminPanel.Models.Pagination;
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

        [HttpGet]
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

            var result = new BasePagedModel<Ware>
            {
                Total = total,
                Page = page.Value,
                PageItems = items
            };

            return PartialView(result);
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
                CategoryId = body.CategoryId,
                InStock = body.InStock
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

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var categories = await _wareCategoryRepository.GetAllAsync();

            var item = await Repository.GetByIdAsync(id);

            ViewBag.Categories = categories;

            return PartialView(item);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] UpdateWareDto body)
        {
            var item = await Repository.GetByIdAsync(body.Id);

            item.Name = body.Name;
            item.RetailPrice = body.RetailPrice;
            item.Price = body.Price;
            item.Discounted = body.Discounted;
            item.Description = body.Description;
            item.CategoryId = body.CategoryId;
            item.InStock = body.InStock;

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