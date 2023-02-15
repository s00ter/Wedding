using Wedding.DAL.Data.Entities.Abstractions;

namespace Wedding.DAL.Data.Entities;

public class Ware : AuditableEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public decimal RetailPrice { get; set; }

    public decimal Price { get; set; }

    public bool Discounted { get; set; }

    public string Description { get; set; }

    public Guid CategoryId { get; set; }

    public byte[] FileBytes { get; set; }


    public WareCategory Category { get; set; }

    public List<OrderWare> OrderWares { get; set; }
}