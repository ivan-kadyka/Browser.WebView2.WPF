using Browser.Commands;
using Browser.Core;
using Browser.Core.Navigation;
using Browser.Messages.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Browser.Module;

public static class BrowserModule
{
    public static IServiceCollection AddBrowserServices(this IServiceCollection services)
    {
       services.AddSingleton<IBrowser, Browser>();
       services.AddSingleton<IBrowserRouter>(sp => sp.GetRequiredService<IBrowser>());
       services.AddSingleton<IBrowserPathRouter>(sp => sp.GetRequiredService<IBrowser>());
       services.AddSingleton<IBrowserObservable>(sp => sp.GetRequiredService<IBrowser>());
       
       // Commands
       services.AddTransient<AddBrowserPageCommand>();
       services.AddTransient<RemoveBrowserPageCommand>();
       
       services.AddMessagesServices();
       
       return services;
    }
}