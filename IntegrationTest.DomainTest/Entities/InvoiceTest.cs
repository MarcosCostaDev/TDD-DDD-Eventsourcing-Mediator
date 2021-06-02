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
            var sut = new Invoice();

            Assert.True(sut.IsValid);
            Assert.Equal(DateTime.Now.Date, sut.CreatedDate.Date);
            Assert.NotEqual(Guid.Empty, sut.Id);
        }
    }
}
