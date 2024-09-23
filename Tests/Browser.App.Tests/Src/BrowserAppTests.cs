using Browser.Abstractions.Page.Factory;
using Browser.WebPage.Wpf.Factory;
using BrowserApp;
using BrowserApp.Module;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Web.WebView2.Wpf;

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
    }
    
    [Fact]
    public void ResolveMainPresenter_Success()
    {
        var mainPresenter = _serviceProvider.GetRequiredService<MainPresenter>();
        
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