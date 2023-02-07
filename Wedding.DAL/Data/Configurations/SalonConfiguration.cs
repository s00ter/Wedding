using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Extensions;

namespace Wedding.DAL.Data.Configurations;

public class SalonConfiguration : IEntityTypeConfiguration<Salon>
{
    public void Configure(EntityTypeBuilder<Salon> builder)
    {
        builder.ConfigureAuditProperties();

        builder
            .HasOne(s => s.City)
            .WithMany(c => c.Salons)
            .HasForeignKey(s => s.CityId);
    }
}