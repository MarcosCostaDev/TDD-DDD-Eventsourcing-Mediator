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
            modelBuilder
                .Ignore<Notification>()
                .Ignore<Notifiable<Notification>>()
                .ApplyConfiguration(new InvoiceMapping())
                .ApplyConfiguration(new ProductMapping())
                .ApplyConfiguration(new InvoiceProductMapping());

            base.OnModelCreating(modelBuilder);

            modelBuilder.SeedProducts();


        }
    }
}
