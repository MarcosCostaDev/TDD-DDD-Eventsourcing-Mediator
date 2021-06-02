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
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Product).WithMany(p => p.InvoiceProducts).HasForeignKey(p => p.ProductId);
            builder.HasOne(p => p.Invoice).WithMany(p => p.InvoiceProducts).HasForeignKey(p => p.InvoiceId);

        }
    }
}
