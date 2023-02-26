using Wedding.DAL.Data.Entities.Abstractions;
using Wedding.DAL.Data.Entities.Enums;

namespace Wedding.DAL.Data.Entities;

public class Order : AuditableEntity
{
    public Guid Id { get; set; }

    public string UserId { get; set; }

    public decimal Total { get; set; }

    public decimal AccruedBonuses { get; set; }

    public decimal RemovedBonuses { get; set; }

    public OrderStatus Status { get; set; }

    public string PaymentMethod { get; set; }

    public string Phone { get; set; }

    public int? PickupSalonId { get; set; }


    public Salon PickupSalon { get; set; }

    public Client Client { get; set; }

    public List<OrderService> OrderServices { get; set; }

    public List<OrderWare> OrderWares { get; set; }
}