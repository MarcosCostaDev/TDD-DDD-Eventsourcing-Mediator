using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.DomainTest.Fixtures
{
    public class TestServerFixture : IDisposable
    {
        public TestServer Server { get; }

        public HttpClient Client { get; }

        public TestServerFixture()
        {
            var root = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            var builder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<TestStartup>();

            Server = new TestServer(builder);

            Client = Server.CreateClient();
        }

        public void Dispose()
        {
            Server.Dispose();
            Client.Dispose();
        }
    }
}
