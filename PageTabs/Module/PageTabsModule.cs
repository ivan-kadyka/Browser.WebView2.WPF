using Microsoft.Extensions.DependencyInjection;
using PresenterBase.Presenter;

namespace PageTabs.Module;

public static class PageTabsModule
{
    public const string PresenterName = "PageTabsPresenter";
    
    public static IServiceCollection AddPageTabsServices(this IServiceCollection services)
    {
        services.AddTransient<PageView>();
        services.AddTransient<PageViewModel>();
        services.AddKeyedSingleton<IPresenter, PagePresenter>(PresenterName);
        
        return services;
    }
}