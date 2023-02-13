namespace Wedding.Web.Models.WareCategory
{
    public class SearchWareResult
    {
        public int Total { get; set; }

        public List<SearchWareResultItem> Items { get; set; }
    }

    public class SearchWareResultItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public bool Discounted { get; set; }
    }
}
