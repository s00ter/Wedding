using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Data.Entities.Enums;
using Wedding.DAL.Repository.Abstractions;
using Wedding.Web.Models.Order;

namespace Wedding.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IWareRepository _wareRepository;

    public OrderController(IOrderRepository orderRepository,
        IWareRepository wareRepository)
    {
        _orderRepository = orderRepository;
        _wareRepository = wareRepository;
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
        var ids = body.OrderItems
            .Select(i => i.WareId)
            .ToList();

        var ware = await _wareRepository.GetQuery()
            .Where(r => ids.Contains(r.Id))
            .ToListAsync();

        var wares = ware
            .Select(t => new OrderWare
            {
                Total = t.RetailPrice * body.OrderItems.First(i => i.WareId == t.Id).Quantity,
                Count = body.OrderItems.First(i => i.WareId == t.Id).Quantity,
                WareId = t.Id
            })
            .ToList();

        var item = new Order
        {
            Phone = body.Phone,
            PaymentMethod = body.Phone,
            OrderWares = wares,
            Total = wares.Sum(t => t.Total),
            Status = OrderStatus.New
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