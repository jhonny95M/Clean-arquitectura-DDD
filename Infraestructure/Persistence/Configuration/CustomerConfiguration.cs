
using Domain.Customers;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c=>c.Id);
        builder.Property(c=>c.Id).HasConversion(
            customerId=>customerId.value,
            value=>new CustomerId(value));
            builder.Property(c=>c.Name).HasMaxLength(50);
            builder.Property(c=>c.LastName).HasMaxLength(50);
            builder.Ignore(c=>c.FullName);
            builder.HasIndex(c=>c.Email).IsUnique();
            builder.Property(c=>c.PhoneNumber).HasConversion(
                phoneNumber=>phoneNumber.Value,
                value=>PhoneNumber.Create(value)!
            ).HasMaxLength(9);
            builder.OwnsOne(c=>c.Address,addressBuilder=>{
                addressBuilder.Property(c=>c.Country).HasMaxLength(3);
                addressBuilder.Property(c=>c.Line1).HasMaxLength(20);
                addressBuilder.Property(c=>c.Line2).HasMaxLength(20).IsRequired(false);
                addressBuilder.Property(c=>c.City).HasMaxLength(40);
                addressBuilder.Property(c=>c.State).HasMaxLength(40);
                addressBuilder.Property(c=>c.ZipCode).HasMaxLength(20).IsRequired(false);
            });
            builder.Property(c=>c.Active);
    }
}