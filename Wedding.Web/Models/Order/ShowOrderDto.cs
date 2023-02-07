namespace Wedding.Web.Models.Order
{
    public class ShowOrderDto
    {
        public string UserId { get; set; }

        public decimal Total { get; set; }

        public int? PickupSalonId { get; set; }
    }
}
