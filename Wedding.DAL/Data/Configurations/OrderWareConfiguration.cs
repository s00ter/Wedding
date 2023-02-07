using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Extensions;

namespace Wedding.DAL.Data.Configurations;

public class OrderWareConfiguration : IEntityTypeConfiguration<OrderWare>
{
    public void Configure(EntityTypeBuilder<OrderWare> builder)
    {
        builder.ConfigureAuditProperties();

        builder
            .HasKey(ow => new { ow.WareId, ow.OrderId });

        builder
            .HasOne(ow => ow.Order)
            .WithMany(s => s.OrderWares)
            .HasForeignKey(ow => ow.OrderId);

        builder
            .HasOne(ow => ow.Ware)
            .WithMany(s => s.OrderWares)
            .HasForeignKey(ow => ow.WareId);
    }
}