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
    public class ProductTest : BaseTest
    {
        public ProductTest(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("spoon", "")]
        [InlineData("", "knife")]
        public void Product_returnsFalse(string name, string brand)
        {
            var sut = new Product(name, brand);

            Assert.False(sut.IsValid);
            Assert.Equal(name, sut.Name);
            Assert.Equal(brand, sut.Brand);
        }

        [Theory]
        [InlineData("knife", "brand-ten")]
        [InlineData("spoon", "maximun")]
        [InlineData("spoon", "underscore")]
        public void Product_returnsTrue(string name, string brand)
        {
            var sut = new Product(name, brand);

            Assert.True(sut.IsValid);
            Assert.Equal(name, sut.Name);
            Assert.Equal(brand, sut.Brand);
        }
    }
}
