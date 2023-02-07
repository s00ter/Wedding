using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Extensions;

namespace Wedding.DAL.Data.Configurations;

public class WareConfiguration : IEntityTypeConfiguration<Ware>
{
    public void Configure(EntityTypeBuilder<Ware> builder)
    {
        builder.ConfigureAuditProperties();

        builder
            .HasOne(w => w.Category)
            .WithMany(c => c.Wares)
            .HasForeignKey(w => w.CategoryId);
    }
}