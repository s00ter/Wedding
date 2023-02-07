using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wedding.DAL.Data.Entities.Abstractions;

namespace Wedding.DAL.Extensions;

public static class EntityTypeConfigurationExtensions
{
    public static EntityTypeBuilder<TAuditableEntity> ConfigureAuditProperties<TAuditableEntity>(this EntityTypeBuilder<TAuditableEntity> builder)
        where TAuditableEntity : class, IAuditableEntity
    {
        builder.Property(auditableEntity => auditableEntity.CreatedAt)
            .IsRequired()
            .HasColumnType("datetime2(6)")
            .HasDefaultValueSql("SYSDATETIMEOFFSET() AT TIME ZONE 'UTC'");

        builder.Property(auditableEntity => auditableEntity.CreatedBy)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(auditableEntity => auditableEntity.UpdatedAt)
            .HasColumnType("datetime2(6)");

        builder.Property(auditableEntity => auditableEntity.UpdatedBy)
            .HasMaxLength(512)
            .IsRequired(false);

        return builder;
    }
}