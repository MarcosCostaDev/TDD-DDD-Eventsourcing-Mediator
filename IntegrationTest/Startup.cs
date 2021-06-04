using AutoMapper;
using IntegrationTest.Domain.Commands.Inputs;
using IntegrationTest.Domain.Repository;
using IntegrationTest.Infra.Contexts;
using IntegrationTest.Infra.Repository;
using IntegrationTest.Infra.UnitOfWork;
using IntegrationTest.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IntegrationTest", Version = "v1" });
            });


            services.AddTransient<IInvoiceRepository, InvoiceRepository>()
                    .AddTransient<IProductRepository, ProductRepository>()
                    .AddTransient<IInvoiceProductsRepository, InvoiceProductsRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ProductCommands, ProductCommands>();
            SetUpDatabase(services);
            SetUpAutoMapper(services);

        }

        protected virtual void SetUpAutoMapper(IServiceCollection services)
        {
                var mapperConfig = new MapperConfiguration(cfg =>
                {
                    // map properties with a public or private getter
                    // https://docs.automapper.org/en/stable/Configuration.html#global-property-field-filtering
                    cfg.ShouldMapProperty = pi => pi.GetMethod != null && (pi.GetMethod.IsPublic || pi.GetMethod.IsPrivate);

                    cfg.AddProfile(new DomainMapping());
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
        }
    }
}
