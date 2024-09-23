using Browser.Abstractions;
using Browser.Abstractions.Page.Factory;
using Browser.WebPage.Wpf.Factory;
using Browser.WebPage.Wpf.Presenters.Container;
using Microsoft.Extensions.DependencyInjection;
using PresenterBase.Presenter;
using PresenterBase.View;

namespace Browser.WebPage.Wpf.Module;

public static class BrowserPageModule
{
    public const string PresenterName = "BrowserPageContainerPresenter";
    internal const string ViewName = "BrowserPageContainerView";
    
    public static IServiceCollection AddBrowserPageServices(this IServiceCollection services)
    {
        services.AddKeyedTransient<IView, PageContainerView>(ViewName);
        services.AddKeyedSingleton<IPresenter, PageContainerPresenter>(PresenterName);
        
        services.AddKeyedSingleton<IPresenter, PageContainerPresenter>(PresenterName, (c, _) => 
            new PageContainerPresenter(
                c.GetRequiredService<IBrowser>(),
                c.GetRequiredKeyedService<IView>(ViewName)));
        
        services.AddSingleton<IWebViewFactory, WebViewFactory>();
        services.AddSingleton<IBrowserPageFactory, BrowserPageFactory>();
        
        return services;
    }
}