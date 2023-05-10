using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using TheProject.Domain.Helpers;

namespace TheProject.Api.Test;

public class StartupFixture : WebApplicationFactory<Startup>, IDisposable
{
    private HttpClient _httpClient;
    public HttpClient Client
    {
        get
        {
            if (_httpClient == null)
                _httpClient = base.Server.CreateClient();
            return _httpClient;
        }
        set
        {
            _httpClient = value;
        }
    }
    private IConfiguration Configuration;

    public StartupFixture() : base()
    {
        var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                          .AddJsonFile("appsettings.Testing.json", optional: false, reloadOnChange: true);

        Configuration = builder.Build();

        DropTestDatabases();

        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        {
            ContractResolver = new NonPublicPropertiesResolver()
        };
    }

    private void DropTestDatabases()
    {
        var appdb = new SqliteConnection(Configuration.GetConnectionString("dbconnection"));

        if (File.Exists(appdb.DataSource))
            File.Delete(appdb.DataSource);

        var hangfiredb = new SqliteConnection(Configuration.GetConnectionString("hangfiredb"));

        if (File.Exists(hangfiredb.DataSource))
            File.Delete(hangfiredb.DataSource);
    }


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseContentRoot(Environment.CurrentDirectory)
                        .UseEnvironment("Testing")
                        .UseConfiguration(Configuration);
        base.ConfigureWebHost(builder);
    }

    public void Dispose()
    {
        Dispose(true);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Server.Dispose();
            Client.Dispose();
        }
    }
}

[CollectionDefinition("API", DisableParallelization = true)]
public class ProjectApiCollection : ICollectionFixture<StartupFixture>
{
}
