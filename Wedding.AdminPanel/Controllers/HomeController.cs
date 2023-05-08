using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wedding.DAL.Data.Entities.Enums;
using Wedding.DAL.Repository.Abstractions;

namespace Wedding.AdminPanel.Controllers;

public class HomeController : Controller
{
    private readonly IOrderRepository _orderRepository;

    public HomeController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    public class StatisticEntry
    {
        public string Status { get; set; }

        public decimal Total { get; set; }
    }

    public async Task<IActionResult> Statistics(string date)
    {
        var splattedDate = date.Split('.');
        var month = int.Parse(splattedDate[0]);
        var year = int.Parse(splattedDate[1]);

        var orders = await _orderRepository.GetQuery()
            .Where(order => order.CreatedAt.Year == year && order.CreatedAt.Month == month)
            .Include(order => order.Client)
            .Select(order =>
                new
                {
                    order.Status,
                    order.Total
                })
            .ToListAsync();

        var mappedOrders = orders
            .Select(order => new StatisticEntry
            {
                Status = StatusToName(order.Status),
                Total = order.Total
            })
            .ToList();

        return PartialView(mappedOrders);
    }

    private static string StatusToName(OrderStatus status)
    {
        return status switch
        {
            OrderStatus.InProgress => "В процессе",
            OrderStatus.New => "Новый",
            OrderStatus.Finished => "Завершён",
            OrderStatus.Cancelled => "Отменён",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}