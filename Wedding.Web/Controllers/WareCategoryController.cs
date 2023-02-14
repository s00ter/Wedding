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
                .Select(i => new WareCategoryDto { Id = i.Id, Name = i.Name })
                .ToList();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetWaresByCategory(SearchWareBody body)
        {
            var waresResult = await _wareCategoryRepository
                .GetWaresByFilter(body.Skip, body.Take, body.CategoryId, body.PriceFrom, body.PriceTo, body.Search);

            var mappedWares = waresResult.Wares
                .Select(x => new SearchWareResultItem
                {
                    Description = x.Description,
                    Id = x.Id,
                    Discounted = x.Discounted,
                    //TODO: add image from db
                    Image = string.Empty,
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
    }
}