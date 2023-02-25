using Wedding.Web.Models.City;

namespace Wedding.Web.Models.Salon;

public class SalonInfoDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public CityDto City { get; set; }
}