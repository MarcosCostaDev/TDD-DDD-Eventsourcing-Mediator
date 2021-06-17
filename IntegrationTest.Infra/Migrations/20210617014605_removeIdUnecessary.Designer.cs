﻿// <auto-generated />
using System;
using IntegrationTest.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IntegrationTest.Infra.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20210617014605_removeIdUnecessary")]
    partial class removeIdUnecessary
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("IntegrationTest.Domain.Entities.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Discount")
                        .HasColumnType("REAL");

                    b.Property<double>("Total")
                        .HasColumnType("REAL");

                    b.Property<double>("TotalWithDiscount")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("IntegrationTest.Domain.Entities.InvoiceProduct", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("InvoiceId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Quantity")
                        .HasColumnType("REAL");

                    b.Property<int>("TempId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProductId", "InvoiceId");

                    b.HasAlternateKey("TempId");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoiceProducts");
                });

            modelBuilder.Entity("IntegrationTest.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Brand")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("41c0f761-70c9-42a4-a0bc-058c1ecf4d57"),
                            Brand = "Nestle",
                            Name = "Nescau",
                            Price = 4.5
                        },
                        new
                        {
                            Id = new Guid("24911ce9-9174-4613-8ea0-91e6ab4a9f6f"),
                            Brand = "Toddy",
                            Name = "Toddynho",
                            Price = 2.5
                        },
                        new
                        {
                            Id = new Guid("0399951c-a322-4298-90cd-712958392496"),
                            Brand = "Coke",
                            Name = "Fanta",
                            Price = 7.2999999999999998
                        },
                        new
                        {
                            Id = new Guid("33903b03-f351-4d7e-bec5-121444f38444"),
                            Brand = "Coke",
                            Name = "Coke",
                            Price = 9.25
                        },
                        new
                        {
                            Id = new Guid("5e70170e-884d-4e73-ae4a-8855e19e349c"),
                            Brand = "Health-option",
                            Name = "Double Flex bread",
                            Price = 5.5
                        });
                });

            modelBuilder.Entity("IntegrationTest.Domain.Entities.InvoiceProduct", b =>
                {
                    b.HasOne("IntegrationTest.Domain.Entities.Invoice", "Invoice")
                        .WithMany("InvoiceProducts")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IntegrationTest.Domain.Entities.Product", "Product")
                        .WithMany("InvoiceProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("IntegrationTest.Domain.Entities.Invoice", b =>
                {
                    b.Navigation("InvoiceProducts");
                });

            modelBuilder.Entity("IntegrationTest.Domain.Entities.Product", b =>
                {
                    b.Navigation("InvoiceProducts");
                });
#pragma warning restore 612, 618
        }
    }
}