using AutoMapper;
using Hangfire;
using Hangfire.SQLite;
using IntegrationTest.BackgroundManager.Actions;
using IntegrationTest.Bus;
using IntegrationTest.Core.Bus;
using IntegrationTest.Domain.Commands.Inputs;
using IntegrationTest.Domain.Mapper;
using IntegrationTest.Domain.Repositories;
using IntegrationTest.Infra.Contexts;
using IntegrationTest.Infra.Repositories;
using IntegrationTest.Mapper;
using IntegrationTest.Security.Hangfire;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;

namespace IntegrationTest
{
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

            services.AddScoped<IMediatorHandler, InMemoryBus>()
                    .AddTransient<IFakingProductAction, FakingProductAction>()
                    .AddTransient<IFakingInvoiceAction, FakingInvoiceAction>()
                    .AddTransient<IInvoiceRepository, InvoiceRepository>()
                    .AddTransient<IProductRepository, ProductRepository>()
                    .AddTransient<IInvoiceProductsRepository, InvoiceProductsRepository>();


            services.AddTransient<ProductCommands, ProductCommands>();
            SetUpDatabase(services);
            SetUpAutoMapper(services);
            services.AddMediatR(typeof(InvoiceCommands));
            ConfigureHangFire(services);

        }

        

        protected virtual void SetUpAutoMapper(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                    // map properties with a public or private getter
                    // https://docs.automapper.org/en/stable/Configuration.html#global-property-field-filtering
                    cfg.ShouldMapProperty = pi => pi.GetMethod != null && (pi.GetMethod.IsPublic || pi.GetMethod.IsPrivate);

                cfg.AddProfile(new DomainMapping());
                cfg.AddProfile(new CommandProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        protected virtual void SetUpDatabase(IServiceCollection services)
        {
            services.AddDbContext<MyDbContext>(optionsBuilder =>
                      optionsBuilder.UseSqlite(Configuration["ConnectionStrings:dbconnection"])
                      , ServiceLifetime.Scoped);
        }

        public virtual void EnsureDbCreated(MyDbContext dbContext)
        {
            try
            {
                dbContext.Database.Migrate();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
    .CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<MyDbContext>();
                EnsureDbCreated(dbContext);
            }

            UseHangfire(app);
        }

       

        protected virtual void ConfigureHangFire(IServiceCollection services)
        {
            services.AddHangfire(options =>
            {
                options.UseSQLiteStorage(Configuration.GetConnectionString("hangfiredb"));
            });

            services.AddHangfireServer(options =>
            {
                options.Queues = new string[] {
                    "generate_fake_products",
                    "generate_fake_invoices"
                };

                options.WorkerCount = 1;
            });
        }

        protected virtual void UseHangfire(IApplicationBuilder app)
        {
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new AllowConnectionsFilter() },
                IgnoreAntiforgeryToken = true
            });

            RecurringJob.AddOrUpdate<IFakingProductAction>($"FakingProducts",
                  x => x.GenerateProducts(), "*/30 * * * * *", queue: "generate_fake_products");

            RecurringJob.AddOrUpdate<IFakingInvoiceAction>($"FakingInvoices",
                  x => x.GenerateInvoices(), "*/1 * * * *", queue: "generate_fake_invoices");
        }
    }
}
