using Browser.TopPanel.Wpf.NavigationPanel;
using Browser.TopPanel.Wpf.SearchBar;
using Browser.TopPanel.Wpf.TabsPanel;
using Microsoft.Extensions.DependencyInjection;
using PresenterBase.Presenter;

namespace Browser.TopPanel.Wpf.Module;

public static class TopPanelModule
{
    public const string PresenterName = "TopPanelPresenter";
    
    public static IServiceCollection AddTopPanelServices(this IServiceCollection services)
    {
        services.AddTransient<TopPanelView>();
        services.AddTransient<TopPanelViewModel>();
        services.AddKeyedSingleton<IPresenter, TopPanelPresenter>(PresenterName);
        
        services.AddTransient<TabsPanelViewModel>();
        services.AddTransient<NavigationPanelViewModel>();
        services.AddTransient<SearchBarViewModel>();
        
        return services;
    }
}