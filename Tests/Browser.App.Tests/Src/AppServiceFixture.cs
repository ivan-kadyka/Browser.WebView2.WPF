using Browser.Abstractions.Page.Factory;
using Browser.App.Tests.Utils;
using Browser.TopPanel.Wpf.Module;
using Browser.WebPage.Wpf.Factory;
using Browser.WebPage.Wpf.Module;
using BrowserApp;
using BrowserApp.Module;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Web.WebView2.Wpf;
using PresenterBase.View;

namespace Browser.App.Tests;

public class AppServiceFixture : IAsyncLifetime
{
    private readonly AppServiceProvider _serviceProvider;
    private readonly MainPresenter _mainPresenter;

    public AppServiceFixture()
    {
        _serviceProvider = AppServiceProvider.CreateTestInstance(OverrideServices);
        _mainPresenter = _serviceProvider.GetRequiredService<MainPresenter>();
    }
    
    public  async Task InitializeAsync()
    {
       await _mainPresenter.Start();
    }
    
    private void OverrideServices(IServiceCollection services)
    {
        services.AddSingleton<IWebViewFactory, WebViewFactoryMock>();
        services.AddSingleton<IBrowserPageFactory, WebPageFactoryMock>();
        services.AddTransient<IMainWindow>(_ =>  Substitute.For<IMainWindow>());
        services.AddKeyedTransient<IView>(TopPanelModule.ViewName, (_, _) => Substitute.For<IView>());
        services.AddKeyedTransient<IView>(BrowserPageModule.ViewName, (_, _) => Substitute.For<IView>());
        
        services.AddSingleton(new TestExceptionProxy());
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

internal class WebViewFactoryMock : IWebViewFactory
{
    public IWebView2 Create(IPageCreateOptions options)
    {
        return Substitute.For<IWebView2>();
    }
}