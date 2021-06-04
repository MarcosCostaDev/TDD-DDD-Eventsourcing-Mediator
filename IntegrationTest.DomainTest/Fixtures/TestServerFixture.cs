using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;

namespace IntegrationTest.DomainTest.Fixtures
{
    public class TestServerFixture : IDisposable
    {
        public TestServer Server { get; }

        public HttpClient Client { get; }

        public TestServerFixture()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.Testing.json", optional: true, reloadOnChange: true)
            .Build();

            var builder = new WebHostBuilder()
               .UseContentRoot(Directory.GetCurrentDirectory())
               .UseEnvironment("Testing")
               .ConfigureServices(services => services.AddSingleton<IConfiguration>(configuration))
               .UseStartup<TestStartup>();

            Server = CreateTestServer(builder);

            Client = Server.CreateClient();
        }


        protected virtual TestServer CreateTestServer(IWebHostBuilder builder)
        {
            return new TestServer(builder);
        }

        public void Dispose()
        {
            Server.Dispose();
            Client.Dispose();
        }
    }
}
