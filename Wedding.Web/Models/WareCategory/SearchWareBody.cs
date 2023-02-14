namespace Wedding.Web.Models.WareCategory
{
    public class SearchWareBody
    {
        public int Skip { get; set; }

        public int Take { get; set; }

        public Guid CategoryId { get; set; }

        public int? PriceFrom { get; set; }

        public int? PriceTo { get; set; }

        public string Search { get; set; }

        public bool PriceDesc { get; set; }
    }
}