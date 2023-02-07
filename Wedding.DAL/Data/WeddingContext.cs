using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wedding.DAL.Constants;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Data.Entities.Abstractions;

namespace Wedding.DAL.Data;

public class WeddingContext : IdentityDbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public WeddingContext(DbContextOptions<WeddingContext> options,
        IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public WeddingContext()
    {

    }

    public DbSet<City> Cities { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderService> OrderServices { get; set; }

    public DbSet<OrderWare> OrderWares { get; set; }

    public DbSet<Salon> Salons { get; set; }

    public DbSet<Service> Services { get; set; }

    public DbSet<Ware> Wares { get; set; }

    public DbSet<WareCategory> WareCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is AuditableEntity or IAuditableEntity && e.State is EntityState.Added or EntityState.Modified)
            .Select(e => new {e.State, Entity = (IAuditableEntity) e.Entity})
            .ToList();

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Entity.CreatedAt = AuditConstants.CreatedUpdatedAtDefaultValue;
                entityEntry.Entity.CreatedBy =
                    _httpContextAccessor?.HttpContext?.User?.Identity?.Name ??
                    AuditConstants.CreatedUpdatedByDefaultValue;
            }
            else
            {
                Entry(entityEntry.Entity).Property(p => p.CreatedAt).IsModified = false;
                Entry(entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
            }

            entityEntry.Entity.UpdatedAt = AuditConstants.CreatedUpdatedAtDefaultValue;
            entityEntry.Entity.UpdatedBy =
                _httpContextAccessor?.HttpContext?.User?.Identity?.Name ??
                AuditConstants.CreatedUpdatedByDefaultValue;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}