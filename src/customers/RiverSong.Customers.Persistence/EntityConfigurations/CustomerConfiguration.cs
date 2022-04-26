using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiverSong.Customers.Domain.Entities;

namespace RiverSong.Customers.Persistence.EntityConfigurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode();

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode();

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(300)
            .IsUnicode();

        builder.IsAuditable();
    }
}