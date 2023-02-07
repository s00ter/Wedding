using Wedding.DAL.Data.Entities.Abstractions;

namespace Wedding.DAL.Data.Entities;

public class OrderWare : AuditableEntity
{
    public Guid OrderId { get; set; }

    public Guid WareId { get; set; }

    public decimal Total { get; set; }

    public int Count { get; set; }


    public Ware Ware { get; set; }

    public Order Order { get; set; }
}