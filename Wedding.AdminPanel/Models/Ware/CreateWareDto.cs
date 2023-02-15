namespace Wedding.AdminPanel.Models.Ware
{
    public class CreateWareDto
    {
        public string Name { get; set; }

        public decimal RetailPrice { get; set; }

        public decimal Price { get; set; }

        public bool Discounted { get; set; }

        public string Description { get; set; }

        public Guid CategoryId { get; set; }

        public IFormFile File { get; set; }
    }
}
