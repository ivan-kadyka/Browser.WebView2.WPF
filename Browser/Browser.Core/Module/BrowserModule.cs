using Browser.Abstractions;
using Browser.Abstractions.Navigation;
using Browser.Core.Commands;
using Browser.Messenger.Module;
using Browser.Settings.Module;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Browser.Core.Module;

public static class BrowserModule
{
    public static IServiceCollection AddBrowserModule(this IServiceCollection services)
    {
       services.AddSingleton<IBrowser, Browser>();
       services.AddSingleton<INavigationRouter>(sp => sp.GetRequiredService<IBrowser>());
       services.AddSingleton<IPathObservable>(sp => sp.GetRequiredService<IBrowser>());
       services.AddSingleton<IBrowserObservable>(sp => sp.GetRequiredService<IBrowser>());
       
       // Commands
       services.AddTransient<CreateBrowserPageCommand>();
       services.AddTransient<RemoveBrowserPageCommand>();
       services.AddTransient<SelectBrowserPageCommand>();
       services.AddTransient<ReloadBrowserPageCommand>();
       
       // Modules
       services.AddBrowserSettingsModule()
       .AddBrowserMessengerModule()
       .AddLogging(builder => builder.AddConsole());
       
       return services;
    }
}