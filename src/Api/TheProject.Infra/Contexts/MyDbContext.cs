using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using TheProject.Core.Loggers;
using TheProject.Domain.Entities;
using TheProject.Infra.Seeds;

namespace TheProject.Infra.Contexts
{
    public class MyDbContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InvoiceProduct> InvoiceProducts { get; set; }

        public MyDbContext([NotNull] DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new QueryLoggerProvider());
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.EnableSensitiveDataLogging();
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
