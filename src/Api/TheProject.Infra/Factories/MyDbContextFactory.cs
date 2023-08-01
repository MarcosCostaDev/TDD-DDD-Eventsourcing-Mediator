using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using TheProject.Infra.Contexts;

namespace TheProject.Infra.Factories
{
    public class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {
            var baseAppPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "IntegrationTest");
            var configuration = new ConfigurationBuilder()
             .SetBasePath(baseAppPath)
             .AddJsonFile("appsettings.json")
             .Build();

            var dbContextBuilder = new DbContextOptionsBuilder<MyDbContext>();

            var connectionString = configuration.GetConnectionString("dbconnection");

            connectionString = connectionString.Replace("./", Path.Combine(baseAppPath, "App_Data"));
            dbContextBuilder.UseSqlite(connectionString, b => b.MigrationsAssembly("IntegrationTest.Infra"));

            return new MyDbContext(dbContextBuilder.Options);
        }
    }
}
