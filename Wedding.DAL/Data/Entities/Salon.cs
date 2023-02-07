using Wedding.DAL.Data.Entities.Abstractions;

namespace Wedding.DAL.Data.Entities;

public class Salon : AuditableEntity
{
    public int Id { get; set; }

    public string Address { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public int CityId { get; set; }


    public City City { get; set; }

    public List<Order> Orders { get; set; }
}