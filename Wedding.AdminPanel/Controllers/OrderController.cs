using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wedding.AdminPanel.Constants;
using Wedding.AdminPanel.Controllers.Abstractions;
using Wedding.AdminPanel.Models.Order;
using Wedding.AdminPanel.Models.Pagination;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Data.Entities.Enums;
using Wedding.DAL.Repository.Abstractions;

namespace Wedding.AdminPanel.Controllers;

public class OrderController : ReadWriteControllerBase<Order, Guid, IOrderRepository>
{
    public OrderController(IOrderRepository repository) : base(repository)
    {
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> ChangeStatus(Guid id, OrderStatus status)
    {
        var orderToUpdate = await Repository.GetByIdAsync(id);
        orderToUpdate.Status = status;
        await Repository.Update(orderToUpdate);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("[controller]/{orderId:guid}/Details")]
    public async Task<IActionResult> Details(Guid orderId)
    {
        var orderItems = await Repository.GetQuery()
            .Include(o => o.OrderWares)
                .ThenInclude(ow => ow.Ware)
            .Where(o => o.Id == orderId)
            .SelectMany(o => o.OrderWares)
            .ToListAsync();

        var result = orderItems
            .Select(w => new OrderItemDto
            {
                Id = w.WareId,
                Name = w.Ware.Name,
                Total = w.Total,
                Quantity = w.Count
            })
            .ToList();

        return PartialView(result);
    }

    [HttpGet("[controller]/Table")]
    public async Task<IActionResult> Table(int? page)
    {
        page ??= 1;
        var query = Repository.GetQuery()
            .OrderByDescending(o => o.CreatedAt);

        var total = await query
            .CountAsync();

        var items = await query
            .Skip((page.Value - 1) * PaginationConstants.ElementsOnPage)
            .Take(PaginationConstants.ElementsOnPage)
            .ToListAsync();

        var result = new BasePagedModel<Order>
        {
            Total = total,
            Page = page.Value,
            PageItems = items
        };

        return PartialView(result);
    }
}