using Microsoft.Extensions.DependencyInjection;
using PresenterBase.Presenter;
using TopPanel.TabsPanel;

namespace TopPanel.Module;

public static class TopPanelModule
{
    public const string PresenterName = "TopPanelPresenter";
    
    public static IServiceCollection AddTopPanelServices(this IServiceCollection services)
    {
        services.AddTransient<TopPanelView>();
        services.AddTransient<TopPanelViewModel>();
        services.AddKeyedSingleton<IPresenter, TopPanelPresenter>(PresenterName);
        
        services.AddTransient<TabsPanelViewModel>();
        
        return services;
    }
}