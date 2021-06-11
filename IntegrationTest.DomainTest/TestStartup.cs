using IntegrationTest.Infra.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.Sqlite;
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
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            services
                .AddEntityFrameworkSqlite()
                .AddDbContext<MyDbContext>(
                    options => options.UseSqlite(connection)
                );
        }


        public override void EnsureDbCreated(MyDbContext dbContext)
        {
            dbContext.Database.OpenConnection(); 
            dbContext.Database.EnsureCreated();

            base.EnsureDbCreated(dbContext);
        }

        protected override void ConfigureHangFire(IServiceCollection services)
        {

        }

        protected override void UseHangfire(IApplicationBuilder app)
        {

        }
    }
}
