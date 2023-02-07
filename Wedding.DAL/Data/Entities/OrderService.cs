using Wedding.DAL.Data.Entities.Abstractions;

namespace Wedding.DAL.Data.Entities;

public class OrderService : AuditableEntity
{
    public Guid ServiceId { get; set; }

    public Guid OrderId { get; set; }

    public decimal Total { get; set; }


    public Service Service { get; set; }

    public Order Order { get; set; }
}