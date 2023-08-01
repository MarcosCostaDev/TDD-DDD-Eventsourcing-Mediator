using AutoMapper;
using TheProject.Application.MappingProfiles;

namespace TheProject.Api.CustomConfigs;

public static class AutoMapperConfig
{
    public static IServiceCollection AddCustomAutoMapper(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            // map properties with a public or private getter
            // https://docs.automapper.org/en/stable/Configuration.html#global-property-field-filtering
            cfg.ShouldMapProperty = pi => pi.GetMethod != null && (pi.GetMethod.IsPublic || pi.GetMethod.IsPrivate);

            cfg.AddProfile(new InvoiceMapProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
        return services;
    }

}
