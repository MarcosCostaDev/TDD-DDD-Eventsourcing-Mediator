using Hangfire;
using Hangfire.SQLite;
using TheProject.Api.Security.Hangfire;
using TheProject.BackgroundManager.Actions;

namespace TheProject.Api.CustomConfigs
{
    public static class HangFireConfig
    {
        public static IServiceCollection AddCustomHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            if (!Convert.ToBoolean(configuration["Services:hangfireEnabled"])) return services;

            services.AddHangfire(options =>
            {
                options.UseSQLiteStorage(configuration.GetConnectionString("hangfiredb"), new SQLiteStorageOptions());
            });

            services.AddHangfireServer(options =>
            {
                options.Queues = new string[] {
                    "generate_fake_products",
                    "generate_fake_invoices"
                };

                options.WorkerCount = 1;
            });
            return services;
        }

        public static IApplicationBuilder UseCustomHangfire(this IApplicationBuilder app)
        {
            var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();

            if (!Convert.ToBoolean(configuration["Services:hangfireEnabled"])) return app;

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                DashboardTitle = nameof(TheProject),
                Authorization = new[] { new AllowConnectionsFilter() },
                IgnoreAntiforgeryToken = true
            });

            RecurringJob.AddOrUpdate<IFakingProductAction>($"FakingProducts",
                  x => x.GenerateProducts(), "*/30 * * * * *", queue: "generate_fake_products");

            RecurringJob.AddOrUpdate<IFakingInvoiceAction>($"FakingInvoices",
                  x => x.GenerateInvoices(), "*/1 * * * *", queue: "generate_fake_invoices");

            return app;
        }
    }
}
