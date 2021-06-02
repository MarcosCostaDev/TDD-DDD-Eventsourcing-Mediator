using IntegrationTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTest.DomainTest.Entities
{
    public class InvoiceProductTest : BaseTest
    {
        public InvoiceProductTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void InvoiceProduct_productId_empty_returnFalse()
        {
            var productId = Guid.Empty;
            var invoiceId = Guid.NewGuid();
            var quantity = 1;
            var sut = new InvoiceProduct(productId, invoiceId, quantity);

            Assert.False(sut.IsValid);
            Assert.Equal(productId, sut.ProductId);
            Assert.Equal(invoiceId, sut.InvoiceId);
            Assert.Equal(quantity, sut.Quantity);
        }

        [Fact]
        public void InvoiceProduct_invoice_empty_returnFalse()
        {
            var productId = Guid.NewGuid() ;
            var invoiceId = Guid.Empty;
            var quantity = 1;
            var sut = new InvoiceProduct(productId, invoiceId, quantity);

            Assert.False(sut.IsValid);
            Assert.Equal(productId, sut.ProductId);
            Assert.Equal(invoiceId, sut.InvoiceId);
            Assert.Equal(quantity, sut.Quantity);
        }

        [Fact]
        public void InvoiceProduct_quantity_negative_returnFalse()
        {
            var productId = Guid.NewGuid();
            var invoiceId = Guid.Empty;
            var quantity = -1;
            var sut = new InvoiceProduct(productId, invoiceId, quantity);

            Assert.False(sut.IsValid);
            Assert.Equal(productId, sut.ProductId);
            Assert.Equal(invoiceId, sut.InvoiceId);
            Assert.Equal(quantity, sut.Quantity);
        }

        [Fact]
        public void InvoiceProduct_returnTrue()
        {
            var productId = Guid.NewGuid();
            var invoiceId = Guid.NewGuid();
            var quantity = 30;
            var sut = new InvoiceProduct(productId, invoiceId, quantity);

            Assert.True(sut.IsValid);
            Assert.Equal(productId, sut.ProductId);
            Assert.Equal(invoiceId, sut.InvoiceId);
            Assert.Equal(quantity, sut.Quantity);
        }
    }
}
