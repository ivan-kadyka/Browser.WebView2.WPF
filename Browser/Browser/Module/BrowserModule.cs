using Browser.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Browser.Module;

public static class BrowserModule
{
    public const string Name = "Browser";
    
    public static IServiceCollection AddBrowserServices(this IServiceCollection services)
    {
       services.AddSingleton<IBrowser, Browser>();
        
        return services;
    }
}