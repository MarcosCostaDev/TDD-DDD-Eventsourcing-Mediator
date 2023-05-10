using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TheProject.Api.CustomConfigs;
using TheProject.Application.AppServices;
using TheProject.Application.AppServices.Interfaces;
using TheProject.BackgroundManager.Actions;
using TheProject.Bus;
using TheProject.Core.Bus;
using TheProject.Domain.Repositories;
using TheProject.Infra.Contexts;
using TheProject.Infra.Repositories;

namespace TheProject.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; protected set; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

        services.AddControllers().AddNewtonsoftJson(x =>
        {
            x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "IntegrationTest", Version = "v1" });
        });

        services.AddDbContext<MyDbContext>(optionsBuilder =>
                optionsBuilder.UseSqlite(Configuration.GetConnectionString("dbconnection"))
                , ServiceLifetime.Scoped);

        services.AddScoped<IMediatorHandler, InMemoryBus>()
                .AddTransient<IFakingProductAction, FakingProductAction>()
                .AddTransient<IFakingInvoiceAction, FakingInvoiceAction>()
                .AddTransient<IInvoiceRepository, InvoiceRepository>()
                .AddTransient<IProductRepository, ProductRepository>()
                .AddTransient<IInvoiceProductsRepository, InvoiceProductsRepository>()
                .AddTransient<IInvoiceAppService, InvoiceAppService>()
                .AddTransient<IProductAppService, ProductAppService>()
                .AddCustomAutoMapper()
                .AddCustomMediatR()
                .AddCustomHangfire(Configuration)
                .AddCustomGlobalization();
    }

    public virtual void EnsureDbCreated(MyDbContext dbContext)
    {
        try
        {
            var appdb = new SqliteConnection(Configuration.GetConnectionString("dbconnection"));

            if (!File.Exists(appdb.DataSource))
            {
                var path = Path.Combine(Environment.CurrentDirectory, "App_Data");

                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            }
            dbContext.Database.EnsureCreated();
        }
        catch (Exception)
        {
        }
        // run Migrations

    }


    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IntegrationTest v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseCustomGlobalization()
           .UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetService<MyDbContext>();
            EnsureDbCreated(dbContext);
        }

        app.UseCustomHangfire();
    }
}
