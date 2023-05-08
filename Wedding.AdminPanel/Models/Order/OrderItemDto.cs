namespace Wedding.AdminPanel.Models.Order;

public class OrderItemDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int Quantity { get; set; }

    public decimal Total { get; set; }
}