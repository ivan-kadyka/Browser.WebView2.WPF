﻿using Browser.Abstractions.Page;
using Browser.Abstractions.Page.Factory;
using Browser.Page.Wpf.Factory;
using Browser.Page.Wpf.Presenters.Container;
using Microsoft.Extensions.DependencyInjection;
using PresenterBase.Presenter;

namespace Browser.Page.Wpf.Module;

public static class BrowserPageModule
{
    public const string PresenterName = "BrowserPageContainerPresenter";
    
    public static IServiceCollection AddBrowserPageServices(this IServiceCollection services)
    {
        services.AddKeyedSingleton<IPresenter, PageContainerPresenter>(PresenterName);
        
        services.AddSingleton<IWebViewFactory, WebViewFactory>();
        services.AddSingleton<IBrowserPageFactory, BrowserPageFactory>();
        
        return services;
    }
}