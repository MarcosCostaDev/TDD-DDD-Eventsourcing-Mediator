using IntegrationTest.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.DomainTest
{
    public class StartupTest : Startup
    {
        public StartupTest(IConfiguration configuration) : base(configuration)
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "appsettings.test.json");
            Configuration = new ConfigurationBuilder()
                                 .AddJsonFile(path, false)
                                 .Build();
        }

        protected override void SetUpDatabase(IServiceCollection services)
        {
            services.AddDbContext<MyDbContext>(optionsBuilder =>
                      optionsBuilder.UseSqlite(Configuration["ConnectionStrings:testdbconnection"])
                      , ServiceLifetime.Scoped);
        }
    }
}
