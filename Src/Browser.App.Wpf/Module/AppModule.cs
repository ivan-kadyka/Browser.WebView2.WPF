using Browser.Abstractions;
using Browser.Core.Module;
using Browser.WebPage.Wpf.Module;
using Microsoft.Extensions.DependencyInjection;
using PresenterBase.Presenter;
using Browser.TopPanel.Wpf.Module;
using BrowserApp.Main;

namespace BrowserApp.Module;

public static class AppModule
{
    public static IServiceCollection AddBrowserAppModule(this IServiceCollection services)
    {
        services.AddBrowserModule()
            .AddTopPanelServices()
            .AddBrowserPageServices();
        
        services.AddTransient<MainViewModel>();
        services.AddTransient<IMainWindow, MainWindow>();
        
        services.AddSingleton<MainPresenter>(c => new MainPresenter(
            c.GetRequiredService<MainViewModel>(),
            c.GetRequiredService<IMainWindow>(),
            c.GetRequiredKeyedService<IPresenter>(TopPanelModule.PresenterName),
            c.GetRequiredKeyedService<IPresenter>(BrowserPageModule.PresenterName),
            c.GetRequiredService<IBrowserObservable>()));

        return services;
    }
}