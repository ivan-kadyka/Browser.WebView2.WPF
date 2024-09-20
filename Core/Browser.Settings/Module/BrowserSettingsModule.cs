using Browser.Settings.Abstractions;
using Browser.Settings.Page;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Browser.Settings.Module;

public static class BrowserSettingsModule
{
    public static IServiceCollection AddBrowserSettingsModule(this IServiceCollection services)
    {
        //TODO IK: Move to appsettings.json
        var inMemoryAppSettings = new Dictionary<string, string>
        {
            { "BrowserSettings:General:HomeAddress", "https://duckduckgo.com" }
        };
        
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemoryAppSettings!)
            .Build();
        
        var browserSettings = new BrowserSettings();
        configuration.GetSection("BrowserSettings").Bind(browserSettings);

        services.AddSingleton<IBrowserSettings>(browserSettings);
        services.AddSingleton<IBrowserPageSettingsProvider, BrowserPageSettingsProvider>();
       
        return services;
    }
}