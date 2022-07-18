using JPN.Core.DomainObjects;
using JPN.Customer.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JPN.Customer.API.Data.Mapping
{
    public class CustomerMapping : IEntityTypeConfiguration<Models.CustomerModel>
    {
        public void Configure(EntityTypeBuilder<Models.CustomerModel> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");
            

            builder.OwnsOne(c => c.Cpf, cs =>
            {
                cs.Property(c => c.Number)
                    .IsRequired()
                    .HasMaxLength(Cpf.CpfMaxLength)
                    .HasColumnName("Cpf")
                    .HasColumnType($"varchar({Cpf.CpfMaxLength})");
            });

            builder.OwnsOne(c => c.Phone, cs =>
            {
                cs.Property(c => c.Number)
                    .IsRequired()
                    .HasMaxLength(Phone.NumerMaxLength)
                    .HasColumnName("Phone")
                    .HasColumnType($"varchar({Phone.NumerMaxLength})");
            });
                
            builder.OwnsOne(c => c.Email, cs =>
            {
                cs.Property(c => c.Adress)
                    .IsRequired()
                    .HasMaxLength(Email.EnderecoMaxLength)
                    .HasColumnName("Email")
                    .HasColumnType($"varchar({Email.EnderecoMaxLength})");
            });

            builder.ToTable("Customer");
        }
    }
}
