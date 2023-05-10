namespace TheProject.Api.CustomConfigs
{
    public static class GlobalizationConfig
    {
        public static IServiceCollection AddCustomGlobalization(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            return services;
        }

        public static IApplicationBuilder UseCustomGlobalization(this IApplicationBuilder app)
        {

            var supportedCultures = new[] { "en-US", "pt-BR", "fr-CA" };
            var localizationOptions = new RequestLocalizationOptions()
                                          .SetDefaultCulture(supportedCultures[0])
                                          .AddSupportedCultures(supportedCultures)
                                          .AddSupportedUICultures(supportedCultures);

            localizationOptions.ApplyCurrentCultureToResponseHeaders = true;

            app.UseRequestLocalization(localizationOptions);


            app.Use((context, next) =>
            {
                string userLangs = "";
                if (context.Request.Headers.ContainsKey("Accept-Language"))
                    userLangs = context.Request.Headers["Accept-Language"].ToString();
                if (context.Request.Query.ContainsKey("culture")) 
                    userLangs = context.Request.Query["culture"].ToString();

                var firstLang = userLangs.Split(',').FirstOrDefault();

                var culture = supportedCultures.Where(p => p.StartsWith(firstLang)).FirstOrDefault("en-US");

                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

                return next();
            });
            return app;
        }
    }
}
