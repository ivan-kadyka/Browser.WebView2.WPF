using Browser.Core;
using Browser.Core.Navigation;
using Microsoft.Extensions.DependencyInjection;

namespace Browser.Module;

public static class BrowserModule
{
    public const string Name = "Browser";
    
    public static IServiceCollection AddBrowserServices(this IServiceCollection services)
    {
       services.AddSingleton<IBrowser, Browser>();
       services.AddSingleton<IBrowserRouter>(sp => sp.GetRequiredService<IBrowser>());
       services.AddSingleton<IBrowserPathRouter>(sp => sp.GetRequiredService<IBrowser>());
        
       return services;
    }
}