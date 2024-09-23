using Browser.Abstractions.Page.Factory;
using Browser.TopPanel.Wpf.Module;
using Browser.WebPage.Wpf.Factory;
using Browser.WebPage.Wpf.Module;
using BrowserApp;
using BrowserApp.Module;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Web.WebView2.Wpf;
using PresenterBase.View;

namespace Browser.App.Tests;

public class BrowserAppTests : IDisposable
{
    private readonly ServiceProvider _serviceProvider;

    public BrowserAppTests()
    {
        _serviceProvider = AppServiceProvider.Create(OverrideServices);   
    }
    
    private void OverrideServices(IServiceCollection services)
    {
        services.AddSingleton<IWebViewFactory, WebViewFactoryMock>();
        services.AddTransient<IMainWindow>(c =>  Substitute.For<IMainWindow>());
        services.AddKeyedTransient<IView>(TopPanelModule.ViewName, (c, _) => Substitute.For<IView>());
        services.AddKeyedTransient<IView>(BrowserPageModule.ViewName, (c, _) => Substitute.For<IView>());
    }
    
    [Fact]
    public async Task MainPresenter_Start_ShouldBeSuccess()
    {
        // Arrange
        var mainPresenter = _serviceProvider.GetRequiredService<MainPresenter>();

        // Act
        await mainPresenter.Start();
        
        // Assert
        Assert.NotNull(mainPresenter);
    }

    public void Dispose()
    {
        _serviceProvider.Dispose();
    }
}

internal class WebViewFactoryMock : IWebViewFactory
{
    public IWebView2 Create(IPageCreateOptions options)
    {
        return Substitute.For<IWebView2>();
    }
}