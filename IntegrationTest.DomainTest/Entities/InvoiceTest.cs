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
    public class InvoiceTest : BaseTest
    {
        public InvoiceTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Invoice_returnsValid()
        {
            var customerId = Guid.NewGuid();
            var sut = new Invoice(customerId);
            AddNotifications(sut);
            Assert.True(sut.IsValid);
            Assert.Equal(DateTime.Now.Date, sut.CreatedDate.Date);
            Assert.Equal(customerId, sut.CustomerId);
            Assert.NotEqual(Guid.Empty, sut.Id);
        }

        [Fact]
        public void CustomerEmpty_returnsInvalid()
        {
            var customerId = Guid.Empty;
            var sut = new Invoice(customerId);
            AddNotifications(sut);
            Assert.False(sut.IsValid);
            Assert.Equal(DateTime.Now.Date, sut.CreatedDate.Date);
            Assert.Equal(customerId, sut.CustomerId);
            Assert.NotEqual(Guid.Empty, sut.Id);
        }


        [Fact]
        public void SetTotal_discount_10_Returns90()
        {
            var customerId = Guid.NewGuid();
            var sut = new Invoice(customerId);

            sut.SetTotal(ListProducts(), 0.1);

            AddNotifications(sut);

            Assert.True(sut.IsValid);
            Assert.Equal(340, sut.Total);
            Assert.Equal(306, sut.TotalWithDiscount);       
            Assert.Equal(0.1, sut.Discount);
            Assert.Equal(DateTime.Now.Date, sut.CreatedDate.Date);
            Assert.Equal(customerId, sut.CustomerId);
            Assert.NotEqual(Guid.Empty, sut.Id);
        }



        private IList<Product> ListProducts()
        {
            return new List<Product>
            {
                new Product("fork", "brand", 100),
                new Product("spoon", "brand", 90),
                new Product("orange", "brand", 80),
                new Product("knife", "brand", 70)

            };
        }
    }
}
