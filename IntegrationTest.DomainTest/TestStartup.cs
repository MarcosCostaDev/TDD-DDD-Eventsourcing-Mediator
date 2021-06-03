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
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
          
        }

        protected override void SetUpDatabase(IServiceCollection services)
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "appsettings.Testing.json");
            var config = new ConfigurationBuilder()
                                .AddJsonFile(path, false)
                                .Build();

            services.AddDbContext<MyDbContext>(optionsBuilder =>
                      optionsBuilder.UseSqlite(config["ConnectionStrings:testdbconnection"])
                      , ServiceLifetime.Scoped);
        }


        public override void EnsureDbCreated(MyDbContext dbContext)
        {
            dbContext.Database.OpenConnection(); 
            dbContext.Database.EnsureCreated();

            //base.EnsureDbCreated(dbContext);
        }
    }
}
