using Microsoft.AspNetCore.Mvc;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Repository.Abstractions;
using Wedding.Web.Models.Ware;

namespace Wedding.Web.Controllers
{
    public class WareController : ControllerBase
    {
        private readonly IWareRepository _wareRepository;

        public WareController(IWareRepository wareRepository)
        {
            _wareRepository = wareRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var items = await _wareRepository.GetAllAsync();

            return Ok(items);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _wareRepository.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Add(WareDto body)
        {
            var item = new Ware
            {
                Name = body.Name,
                Price = body.Price,
                Description = body.Description
            };

            await _wareRepository.Create(item);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var entity = await _wareRepository.GetByIdAsync(id);

            await _wareRepository.Delete(entity);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Ware item)
        {
            await _wareRepository.Update(item);

            return Ok();
        }
    }
}
