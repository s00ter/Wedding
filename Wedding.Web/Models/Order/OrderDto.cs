namespace Wedding.Web.Models.Order;

public class OrderDto
{
    public string Phone { get; set; }

    public string PaymentMethod { get; set; }

    public List<OrderItemDto> OrderItems { get; set; }
}

public class OrderItemDto
{
    public Guid WareId { get; set; }

    public int Quantity { get; set; }
}