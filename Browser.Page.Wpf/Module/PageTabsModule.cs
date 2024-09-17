using Microsoft.Extensions.DependencyInjection;
using PresenterBase.Presenter;

namespace Browser.Page.Wpf.Module;

public static class BrowserPageModule
{
    public const string PresenterName = "BrowserPagePresenter";
    
    public static IServiceCollection AddBrowserPageServices(this IServiceCollection services)
    {
        services.AddTransient<PageView>();
        services.AddTransient<PageViewModel>();
        services.AddKeyedSingleton<IPresenter, PagePresenter>(PresenterName);
        
        return services;
    }
}