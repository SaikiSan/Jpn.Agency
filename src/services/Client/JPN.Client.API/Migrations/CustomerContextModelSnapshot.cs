// <auto-generated />
using System;
using JPN.Customer.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JPN.Customer.API.Migrations
{
    [DbContext(typeof(CustomerContext))]
    partial class CustomerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JPN.Customer.API.Models.CustomerModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("JPN.Customer.API.Models.CustomerModel", b =>
                {
                    b.OwnsOne("JPN.Core.DomainObjects.Cpf", "Cpf", b1 =>
                        {
                            b1.Property<Guid>("CustomerModelId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnName("Cpf")
                                .HasColumnType("varchar(11)")
                                .HasMaxLength(11);

                            b1.HasKey("CustomerModelId");

                            b1.ToTable("Customer");

                            b1.WithOwner()
                                .HasForeignKey("CustomerModelId");
                        });

                    b.OwnsOne("JPN.Core.DomainObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("CustomerModelId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Adress")
                                .IsRequired()
                                .HasColumnName("Email")
                                .HasColumnType("varchar(254)")
                                .HasMaxLength(254);

                            b1.HasKey("CustomerModelId");

                            b1.ToTable("Customer");

                            b1.WithOwner()
                                .HasForeignKey("CustomerModelId");
                        });

                    b.OwnsOne("JPN.Core.DomainObjects.Phone", "Phone", b1 =>
                        {
                            b1.Property<Guid>("CustomerModelId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnName("Phone")
                                .HasColumnType("varchar(14)")
                                .HasMaxLength(14);

                            b1.HasKey("CustomerModelId");

                            b1.ToTable("Customer");

                            b1.WithOwner()
                                .HasForeignKey("CustomerModelId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
