using IntegrationTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Infra.Mappings
{
    public class InvoiceProductMapping : IEntityTypeConfiguration<InvoiceProduct>
    {
        public void Configure(EntityTypeBuilder<InvoiceProduct> builder)
        {
            //TODO: Looking for solution about EF/SQLite that generate a TempID integer when use builder.HasKey(p => new { p.InvoiceId, p.ProductId }); for migration
            builder.HasKey(p => p.Id);           
            builder.Property(p => p.ProductId).IsRequired();
            builder.Property(p => p.InvoiceId).IsRequired();
            builder.HasOne(p => p.Product).WithMany(p => p.InvoiceProducts).HasForeignKey(p => p.ProductId);
            builder.HasOne(p => p.Invoice).WithMany(p => p.InvoiceProducts).HasForeignKey(p => p.InvoiceId);

        }
    }
}
