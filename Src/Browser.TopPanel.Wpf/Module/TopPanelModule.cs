using Browser.TopPanel.Wpf.NavigationPanel;
using Browser.TopPanel.Wpf.SearchBar;
using Browser.TopPanel.Wpf.TabsPanel;
using Microsoft.Extensions.DependencyInjection;
using PresenterBase.Presenter;
using PresenterBase.View;

namespace Browser.TopPanel.Wpf.Module;

public static class TopPanelModule
{
    public const string PresenterName = "TopPanelPresenter";
    internal const string ViewName = "TopPanelView";
    
    public static IServiceCollection AddTopPanelServices(this IServiceCollection services)
    {
        services.AddKeyedTransient<IView, TopPanelView>(ViewName);
        services.AddTransient<TopPanelViewModel>();
        services.AddKeyedSingleton<IPresenter, TopPanelPresenter>(PresenterName);
        
        services.AddKeyedSingleton<IPresenter, TopPanelPresenter>(PresenterName, (c, _) => 
            new TopPanelPresenter(
            c.GetRequiredService<TopPanelViewModel>(),
            c.GetRequiredKeyedService<IView>(ViewName)));
        
        services.AddTransient<TabsPanelViewModel>();
        services.AddTransient<NavigationPanelViewModel>();
        services.AddTransient<SearchBarViewModel>();
        
        return services;
    }
}