using TheProject.Application.EventHandlers;

namespace TheProject.Api.CustomConfigs;

public static class MediatRConfig
{
    public static IServiceCollection AddCustomMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(InvoiceEventHandlers).Assembly));

        return services;
    }
}
