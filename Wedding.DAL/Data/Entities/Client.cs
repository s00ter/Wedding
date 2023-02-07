using Microsoft.AspNetCore.Identity;

namespace Wedding.DAL.Data.Entities;

public class Client : IdentityUser
{
    public decimal AvailableBonuses { get; set; }

    public List<Order> Orders { get; set; }
}