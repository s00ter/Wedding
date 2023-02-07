using Wedding.DAL.Data.Entities.Abstractions;

namespace Wedding.DAL.Data.Entities;

public class Service : AuditableEntity
{
    public Guid Id { get; set; }

    public decimal Price { get; set; }

    public bool Discounted { get; set; }


    public List<OrderService> OrderServices { get; set; }
}