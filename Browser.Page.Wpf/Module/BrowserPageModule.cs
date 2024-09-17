using Browser.Abstractions.Page;
using Browser.Page.Wpf.Factory;
using Browser.Page.Wpf.Page;
using Browser.Page.Wpf.Presenters.Container;
using Microsoft.Extensions.DependencyInjection;
using PresenterBase.Presenter;
using PageView = Browser.Page.Wpf.Presenters.PageView;

namespace Browser.Page.Wpf.Module;

public static class BrowserPageModule
{
    public const string PresenterName = "BrowserPageContainerPresenter";
    
    public static IServiceCollection AddBrowserPageServices(this IServiceCollection services)
    {
        services.AddTransient<PageView>();
        services.AddTransient<PageViewModel>();
        services.AddKeyedSingleton<IPresenter, PageContainerPresenter>(PresenterName);
        
        services.AddSingleton<IWebViewFactory, WebViewFactory>();
        services.AddSingleton<IBrowserPageFactory, BrowserPageFactory>();
        
        return services;
    }
}