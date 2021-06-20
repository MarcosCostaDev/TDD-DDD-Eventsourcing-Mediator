using Flunt.Notifications;
using IntegrationTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntegrationTest.Infra.Mappings
{
    public class InvoiceProductMapping : IEntityTypeConfiguration<InvoiceProduct>
    {
        public void Configure(EntityTypeBuilder<InvoiceProduct> builder)
        {
            builder
               .HasKey(pt => new { pt.InvoiceId, pt.ProductId });

            builder
                .HasOne(pt => pt.Product)
                .WithMany(pt => pt.InvoiceProducts)
                .HasForeignKey(pt => pt.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(pt => pt.Invoice)
                .WithMany(pt => pt.InvoiceProducts)
                .HasForeignKey(pt => pt.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
