using IntegrationTest.DomainTest.Fixtures;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTest.DomainTest.Factories
{
    public class AbstractIntegrationTest : BaseTest, IClassFixture<TestServerFixture>
    {
        public readonly HttpClient Client;
        public readonly TestServer Server;
        protected AbstractIntegrationTest(ITestOutputHelper output, TestServerFixture testServerFixture)
            : base(output)
        {
            Client = testServerFixture.Client;
            Server = testServerFixture.Server;
        }
    }
}
