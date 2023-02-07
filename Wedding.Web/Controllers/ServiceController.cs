using Microsoft.AspNetCore.Mvc;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Repository.Abstractions;
using Wedding.Web.Models.Service;

namespace Wedding.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Add(ServiceDto body)
        {
            var item = new Service
            {
                Discounted = body.Discounted,
                Price = body.Price
            };

            await _serviceRepository.Create(item);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var entity = await _serviceRepository.GetByIdAsync(id);

            await _serviceRepository.Delete(entity);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var items = await _serviceRepository.GetAllAsync();

            return Ok(items);
        }
    }
}

