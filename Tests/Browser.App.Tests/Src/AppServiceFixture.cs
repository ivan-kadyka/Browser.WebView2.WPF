using Browser.TopPanel.Wpf.Module;
using Browser.WebPage.Wpf.Factory;
using Browser.WebPage.Wpf.Module;
using BrowserApp;
using BrowserApp.Module;
using Microsoft.Extensions.DependencyInjection;
using PresenterBase.View;

namespace Browser.App.Tests;

public class AppServiceFixture : IAsyncLifetime
{
    private readonly ServiceProvider _serviceProvider;
    private readonly MainPresenter _mainPresenter;

    public AppServiceFixture()
    {
        _serviceProvider = AppServiceProvider.Create(OverrideServices);
        _mainPresenter = _serviceProvider.GetRequiredService<MainPresenter>();
    }
    
    public  async Task InitializeAsync()
    {
       await _mainPresenter.Start();
    }
    
    private void OverrideServices(IServiceCollection services)
    {
        services.AddSingleton<IWebViewFactory, WebViewFactoryMock>();
        services.AddTransient<IMainWindow>(c =>  Substitute.For<IMainWindow>());
        services.AddKeyedTransient<IView>(TopPanelModule.ViewName, (c, _) => Substitute.For<IView>());
        services.AddKeyedTransient<IView>(BrowserPageModule.ViewName, (c, _) => Substitute.For<IView>());
    }
    
    public T GetService<T>() where T : notnull
    {
        return _serviceProvider.GetRequiredService<T>();
    }

    public async Task DisposeAsync()
    {
       await _mainPresenter.Stop();
       await _serviceProvider.DisposeAsync();
    }
}