using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TheProject.Api.Test.Extensions;
using TheProject.Infra.Contexts;
using Xunit;
using Xunit.Abstractions;

namespace TheProject.Api.Test.Abstracts;

public class AbstractIntegrationTest : AbstractTest, IDisposable
{
    public readonly HttpClient Client;
    public readonly TestServer Server;
    private readonly IServiceScope _serviceScope;

    protected AbstractIntegrationTest(ITestOutputHelper output, StartupFixture testServerFixture)
        : base(output)
    {
        Client = testServerFixture.Client;
        Server = testServerFixture.Server;
        _serviceScope = testServerFixture.Server.Services.CreateScope();
    }

    protected StringContent GetJsonContent(object obj)
    {
        return new StringContent(JsonConvert.SerializeObject(obj, ApiResponseExtensions.GetJsonSerializerOptions()), Encoding.UTF8, new MediaTypeHeaderValue("application/json"));
    }
    public async Task DisposeAsync(bool dispose)
    {
        if (!dispose) return;
        using var appDbContext = _serviceScope.ServiceProvider.GetRequiredService<MyDbContext>();

        var invoices = await appDbContext.InvoiceProducts.ToListAsync();

        appDbContext.InvoiceProducts.RemoveRange(invoices);

        await appDbContext.SaveChangesAsync();

        _serviceScope.Dispose();
    }

    public void Dispose()
    {
        DisposeAsync(true).ConfigureAwait(false).GetAwaiter().GetResult();
    }
}

