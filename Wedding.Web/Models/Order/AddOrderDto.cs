namespace Wedding.Web.Models.Order
{
    public class AddOrderDto
    {

        public decimal Total { get; set; }

        public decimal AccruedBonuses { get; set; }

        public decimal RemovedBonuses { get; set; }

        public int? PickupSalonId { get; set; }
    }
}
