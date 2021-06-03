using IntegrationTest.DomainTest.Factories;
using IntegrationTest.DomainTest.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTest.DomainTest.Api
{
    [Collection("Integration tests collection")]
    public class ProductControllerTest : AbstractIntegrationTest
    {
        public ProductControllerTest(ITestOutputHelper output, TestServerFixture testServerFixture)
         : base(output, testServerFixture)
        {
        }

        [Fact]
        public async void TestVisitRoot()
        {
            var response = await Client.GetAsync("/");
            response.EnsureSuccessStatusCode();
        }
    }
}
