using Microsoft.AspNetCore.Mvc;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Repository.Abstractions;
using Wedding.Web.Models.Order;

namespace Wedding.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var items = await _orderRepository.GetAllAsync();

            return Ok(items);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _orderRepository.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Add(OrderDto body)
        {
            var item = new Order
            {
                Total = body.Price
            };

            await _orderRepository.Create(item);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var entity = await _orderRepository.GetByIdAsync(id);

            await _orderRepository.Delete(entity);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Order item)
        {
            await _orderRepository.Update(item);

            return Ok();
        }
    }
}
