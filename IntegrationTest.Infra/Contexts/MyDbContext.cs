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
                .ApplyConfigurationsFromAssembly(typeof(MyDbContext).Assembly)
                .Ignore<Notification>()
                .Ignore<Notifiable<Notification>>();

            modelBuilder.SeedProducts();
        }
    }
}
