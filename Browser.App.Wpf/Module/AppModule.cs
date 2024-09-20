﻿using Browser.Core.Module;
using Browser.WebPage.Wpf.Module;
using Microsoft.Extensions.DependencyInjection;
using PresenterBase.Presenter;
using Browser.TopPanel.Wpf.Module;

namespace BrowserApp.Module;

public static class AppModule
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddBrowserServices()
            .AddTopPanelServices()
            .AddBrowserPageServices();
        
        services.AddTransient<MainViewModel>();
        
        services.AddSingleton<MainPresenter>(c => new MainPresenter(
            c.GetRequiredService<MainViewModel>(),
            c.GetRequiredKeyedService<IPresenter>(TopPanelModule.PresenterName),
            c.GetRequiredKeyedService<IPresenter>(BrowserPageModule.PresenterName)));

        return services;
    }
}