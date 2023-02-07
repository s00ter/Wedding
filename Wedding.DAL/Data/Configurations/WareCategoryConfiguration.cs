using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Extensions;

namespace Wedding.DAL.Data.Configurations;

public class WareCategoryConfiguration : IEntityTypeConfiguration<WareCategory>
{
    public void Configure(EntityTypeBuilder<WareCategory> builder)
    {
        builder.ConfigureAuditProperties();
    }
}