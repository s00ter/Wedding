using Microsoft.AspNetCore.Mvc;
using Wedding.DAL.Repository.Abstractions;
using Wedding.Web.Models.WareCategory;

namespace Wedding.Web.Controllers
{
    [Route("[controller]")]
    public class WareCategoryController : ControllerBase
    {
        private readonly IWareCategoryRepository _wareCategoryRepository;

        public WareCategoryController(IWareCategoryRepository wareCategoryRepository)
        {
            _wareCategoryRepository = wareCategoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetWareCategories()
        {
            var items = await _wareCategoryRepository.GetAllAsync();

            var result = items
                .Select(i => new WareCategoryDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Image = i.FileBytes == null
                        ? null
                        : Convert.ToBase64String(i.FileBytes, 0, i.FileBytes.Length), })
                .ToList();

            return Ok(result);
        }

        [HttpGet("{categoryId:guid}/Image")]
        public async Task<IActionResult> GetCategoryImage(Guid categoryId)
        {
            var items = await _wareCategoryRepository.GetByIdAsync(categoryId);

            return File(items.FileBytes, "image/jpg");
        }

        [HttpPost]
        public async Task<IActionResult> GetWaresByCategory([FromBody] SearchWareBody body)
        {
            var waresResult = await _wareCategoryRepository
                .GetWaresByFilter(body.Skip, body.Take, body.CategoryId, body.PriceFrom, body.PriceTo, body.Search, body.PriceDesc);

            var mappedWares = waresResult.Wares
                .Select(x => new SearchWareResultItem
                {
                    Description = x.Description,
                    Id = x.Id,
                    Discounted = x.Discounted,
                    Image = x.FileBytes == null
                        ? null
                        : Convert.ToBase64String(x.FileBytes, 0, x.FileBytes.Length),
                    Name = x.Name,
                    Price = x.RetailPrice
                })
                .ToList();

            var result = new SearchWareResult
            {
                Total = waresResult.Total,
                Items = mappedWares
            };

            return Ok(result);
        }

        [HttpGet("PriceRanges")]
        public async Task<IActionResult> GetPriceRanges(Guid categoryId)
        {
            var ranges = await _wareCategoryRepository.GetCategoryPricesRange(categoryId);

            var result = new
            {
                ranges.Min,
                ranges.Max
            };

            return Ok(result);
        }
    }
}