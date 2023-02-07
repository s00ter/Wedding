using Wedding.DAL.Data.Entities.Abstractions;

namespace Wedding.DAL.Data.Entities;

public class City : AuditableEntity
{
    public int Id { get; set; }

    public string Name { get; set; }


    public List<Salon> Salons { get; set; }
}