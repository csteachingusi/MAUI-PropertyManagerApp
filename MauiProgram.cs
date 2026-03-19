using Microsoft.Extensions.Logging;
using PropertyManagerApp.Services;
using PropertyManagerApp.Views;
using Microsoft.Extensions.DependencyInjection;

namespace PropertyManagerApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register HttpClient
            builder.Services.AddHttpClient<PropertyService>();

            // Register pages for DI / navigation
            builder.Services.AddTransient<PropertiesListPage>();
            builder.Services.AddTransient<PropertyDetailPage>();
            builder.Services.AddTransient<AddPropertyPage>();
            builder.Services.AddTransient<EditPropertyPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}