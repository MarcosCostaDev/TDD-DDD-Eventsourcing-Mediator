using Flunt.Notifications;
using IntegrationTest.Domain.Entities;
using IntegrationTest.Infra.Mappings;
using IntegrationTest.Infra.Seeds;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Infra.Contexts
{
    public class MyDbContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InvoiceProduct> InvoiceProducts { get; set; }

        public MyDbContext([NotNull] DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<InvoiceProduct>()
                .HasKey(p => new { p.InvoiceId, p.ProductId });
            modelBuilder.Entity<InvoiceProduct>()
                .HasOne(p => p.Product)
                .WithMany(p => p.InvoiceProducts)
                .HasForeignKey(p => p.ProductId);
            modelBuilder.Entity<InvoiceProduct>()
                .HasOne(p => p.Invoice)
                .WithMany(p => p.InvoiceProducts)
                .HasForeignKey(p => p.InvoiceId);


            modelBuilder
                .Ignore<Notification>()
                .Ignore<Notifiable<Notification>>()
                //.ApplyConfiguration(new InvoiceProductMapping())
                .ApplyConfiguration(new InvoiceMapping())
                .ApplyConfiguration(new ProductMapping());
                

            base.OnModelCreating(modelBuilder);

            modelBuilder.SeedProducts();


        }
    }
}
