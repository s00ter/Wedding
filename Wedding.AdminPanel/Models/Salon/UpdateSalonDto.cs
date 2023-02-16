namespace Wedding.AdminPanel.Models.Salon
{
    public class UpdateSalonDto
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public int CityId { get; set; }

        public IFormFile File { get; set; }
    }
}
