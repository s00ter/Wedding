using Wedding.DAL.Data.Entities.Abstractions;

namespace Wedding.DAL.Data.Entities;

public class Order : AuditableEntity
{
    public Guid Id { get; set; }

    public string UserId { get; set; }

    public decimal Total { get; set; }

    public decimal AccruedBonuses { get; set; }

    public decimal RemovedBonuses { get; set; }

    public int? PickupSalonId { get; set; }


    public Salon PickupSalon { get; set; }

    public Client Client { get; set; }

    public List<OrderService> OrderServices { get; set; }

    public List<OrderWare> OrderWares { get; set; }
}