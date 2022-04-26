using RiverSong.Shared.Domain.Common;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class EntityTypeBuilderExtensions
{
    public static void IsAuditable<T>(this EntityTypeBuilder<T> builder) where T : AuditableEntityBase
    {
        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .IsRequired()
            .HasMaxLength(300)
            .IsUnicode();

        builder.Property(x => x.UpdatedBy)
            .HasMaxLength(300)
            .IsUnicode();
    }
}