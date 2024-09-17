using Browser.Core.Module;
using Microsoft.Extensions.DependencyInjection;
using PageTabs.Module;
using PresenterBase.Presenter;
using Browser.TopPanel.Wpf.Module;

namespace BrowserApp.Module;

public static class AppModule
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddBrowserServices()
            .AddTopPanelServices()
            .AddPageTabsServices();
        
        services.AddTransient<MainViewModel>();
        services.AddTransient<MainWindow>();
        
        services.AddSingleton<MainPresenter>(c => new MainPresenter(
            c.GetRequiredService<MainWindow>(),
            c.GetRequiredService<MainViewModel>(),
            c.GetRequiredKeyedService<IPresenter>(TopPanelModule.PresenterName),
            c.GetRequiredKeyedService<IPresenter>(PageTabsModule.PresenterName)));

        return services;
    }
}