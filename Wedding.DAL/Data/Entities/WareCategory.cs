using Wedding.DAL.Data.Entities.Abstractions;

namespace Wedding.DAL.Data.Entities;

public class WareCategory : AuditableEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public byte[] FileBytes { get; set; }

    public List<Ware> Wares { get; set; }
}