namespace Wedding.AdminPanel.Models.Salon;

public class CreateSalonDto
{
    public string Address { get; set; }

    public int CityId { get; set; }

    public IFormFile File { get; set; }
}