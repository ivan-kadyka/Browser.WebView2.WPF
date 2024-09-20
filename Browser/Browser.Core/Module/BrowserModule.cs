using Browser.Abstractions;
using Browser.Abstractions.Navigation;
using Browser.Abstractions.Page;
using Browser.Core.Commands;
using Browser.Core.Settings;
using Browser.Messenger.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Browser.Core.Module;

public static class BrowserModule
{
    public static IServiceCollection AddBrowserServices(this IServiceCollection services)
    {
       services.AddSingleton<IBrowser, Browser>();
       services.AddSingleton<IBrowserRouter>(sp => sp.GetRequiredService<IBrowser>());
       services.AddSingleton<IBrowserPathRouter>(sp => sp.GetRequiredService<IBrowser>());
       services.AddSingleton<IBrowserObservable>(sp => sp.GetRequiredService<IBrowser>());
       
       // Commands
       services.AddTransient<CreateBrowserPageCommand>();
       services.AddTransient<RemoveBrowserPageCommand>();
       services.AddTransient<SelectBrowserPageCommand>();
       services.AddTransient<ReloadBrowserPageCommand>();
       
       
       // Settings
       services.AddSingleton<IBrowserSettings, BrowserSettings>();
       
       services.AddMessagesServices();
       
       return services;
    }
}