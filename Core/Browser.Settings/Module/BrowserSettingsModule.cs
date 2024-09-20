using Browser.Abstractions.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Browser.Settings.Module;

public static class BrowserSettingsModule
{
    public static IServiceCollection AddBrowserSettingsModule(this IServiceCollection services)
    {

        services.AddSingleton<IBrowserSettings, BrowserSettings>();
       
        return services;
    }
}